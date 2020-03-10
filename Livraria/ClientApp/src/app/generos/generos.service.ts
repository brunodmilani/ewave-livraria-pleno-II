import { Genero } from './generos.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap, delay, take } from 'rxjs/operators';

@Injectable()
export class GenerosService {

  private readonly API = `${environment.API}Genero`;
  constructor(private http: HttpClient) { }

  list() {
    return this.http.get<Genero[]>(this.API)
      .pipe(
        delay(2000),
        tap(console.log)
      );
  }

  get() {
    return this.http.get<Genero[]>(this.API)
      .pipe(
        tap(console.log)
      );
  }

  getGenero(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.get<Genero>(_URL)
      .pipe(
        tap(console.log)
      );
  }

  create(request: Genero) {
    return this.http.post<Genero>(this.API, request).pipe(take(1));
      
  }  

  update(Id: string, request: Genero) {
    const _URL = `${this.API}/${Id}`;
    return this.http.put<Genero>(_URL, request).pipe(take(1));
  }

  delete(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.delete<any>(_URL).pipe(take(1));
  }
}
