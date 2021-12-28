import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.css']
})


export class StarRatingComponent implements OnInit {


  @Input() selectedRating : number = 0;
  @Input() enabled : boolean = true;
  @Input() movieId : string = '';
  @Output() onSelected = new EventEmitter<Ratingitem>();

  message: string = '';

  stars = [
    {
      id: 1,
      icon: 'star',
      class: 'star-gray star-hover star'
    },
    {
      id: 2,
      icon: 'star',
      class: 'star-gray star-hover star'
    },
    {
      id: 3,
      icon: 'star',
      class: 'star-gray star-hover star'
    },
    {
      id: 4,
      icon: 'star',
      class: 'star-gray star-hover star'
    },
    {
      id: 5,
      icon: 'star',
      class: 'star-gray star-hover star'
    }

  ];

  constructor() {}

  ngOnInit(): void {

  }


selectStar(value: number): void{
  if(this.enabled)
  {
         this.stars.filter( (star) => {

          if ( star.id <= value){

            star.class = 'star-gold star';

          }else{

            star.class = 'star-gray star';

          }
          console.log(value);

          return star;
        });


      this.selectedRating = value;

      let ratingItem = new Ratingitem();
      ratingItem.movieId = this.movieId;
      ratingItem.value = value;

      this.onSelected.emit(ratingItem);
      this.message = "Thank you";
      }
    }

}

export class Ratingitem
{
  movieId : string = '';
  value: number = 0
}

