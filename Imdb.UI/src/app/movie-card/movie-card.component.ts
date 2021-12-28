import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MovieDto } from '../data.service';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent {

  constructor() { }

  @Input() movie: MovieDto = new MovieDto();

  @Output() selected: EventEmitter<MovieDto> = new EventEmitter<MovieDto>();

  ngOnInit(): void {
  }

  onClick(): void {
    this.selected.emit(this.movie);
  }

}
