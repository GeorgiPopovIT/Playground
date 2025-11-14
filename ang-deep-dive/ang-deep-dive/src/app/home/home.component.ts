import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { HighlightDirective } from '../directives/highlight.directive';
import { FilmComponent, Film } from '../film/film.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FilmComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent  {
  @Input()
  something! : string;
  titleHome! : string;
  
  constructor(private filmService: FilmComponent) {}

  

  @Output()
  sectionName = new EventEmitter<string>();
  
  
  
}