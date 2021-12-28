import { Component, OnInit } from '@angular/core';
import { DataService , MoviesPagedListResponse } from '../data.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css'],
})
export class MoviesComponent implements OnInit {

  moviesResponse : MoviesPagedListResponse | undefined;
  page: number = 1;
  pageSize: number = 10
  searchTerm: string = '';
  isLogedIn = false;
  constructor(private dataService : DataService) {

  }

  ngOnInit(): void {
    this.dataService.getMoviesByFilter(undefined, 1, 10).subscribe((complete : MoviesPagedListResponse) => {
      this.moviesResponse = complete;
    });

    if(window.sessionStorage.getItem("TOKEN_KEY"))
    {
      this.isLogedIn = true;
    }

  }

  onFilterChange(searchTerm: string) : void
  {
    console.log('search term' + searchTerm);
    this.searchTerm = searchTerm;
    this.dataService.getMoviesByFilter(searchTerm, this.page, this.pageSize).subscribe((complete : MoviesPagedListResponse) => {
      this.moviesResponse = complete;
    });

  }

  loadNext() : void
  {
    console.log(this.page + " " + this,this.pageSize);
    this.page = this.page + 1;
    this.dataService.getMoviesByFilter(this.searchTerm, this.page, this.pageSize).subscribe((complete : MoviesPagedListResponse) => {
      this.moviesResponse = complete;
    });

  }

  loadPrevious() : void
  {
    console.log(this.page + " " + this,this.pageSize);
    this.page = this.page - 1;
    this.dataService.getMoviesByFilter(this.searchTerm, this.page, this.pageSize).subscribe((complete : MoviesPagedListResponse) => {
      this.moviesResponse = complete;
    });

  }

  rateMovie(data : any)
  {

    this.dataService.rateMovie(data.movieId, data.value).subscribe(x => {});
    console.log('movie rated');
    console.log(JSON.stringify(data));
  }

}
