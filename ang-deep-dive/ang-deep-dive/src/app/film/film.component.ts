import { Component, inject, Injectable, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Film {
  title: string;
  episode_id: number;
  opening_crawl: string;
  director: string;
  producer: string;
  release_date: string;
  characters: string[];
  planets: string[];
  starships: string[];
  vehicles: string[];
  species: string[];
  url: string;
}

@Injectable({
  providedIn: 'root'
})
@Component({
  selector: 'app-film',
  imports: [],
  templateUrl: './film.component.html',
  styleUrl: './film.component.css'
})
export class FilmComponent implements OnInit, OnDestroy {
  private apiUrl = 'https://swapi.info/api/films';
  films! : Film[];

  private http = inject(HttpClient);

  constructor() {}
  ngOnDestroy(): void {
    console.log('On Destroy called');
  }
  
  ngOnInit(): void {
    console.log('On Iinit called');
    this.getFilms().subscribe((data: Film[]) => {
      this.films = data;
    });
  }

  getFilms(): Observable<Film[]> {
    return this.http.get<Film[]>(this.apiUrl);
  }
}