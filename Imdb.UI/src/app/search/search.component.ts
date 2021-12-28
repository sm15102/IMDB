import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor() { }

  searchValue : String = "";
  @Output() searchEmiter = new EventEmitter<string>();

  ngOnInit(): void {
  }

  onChange(value: string) {
    console.log(value);
    if(value.length > 2 || value.length < 0)
    {
      this.searchEmiter.emit(value)
    }


}

}
