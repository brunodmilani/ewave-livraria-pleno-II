import { Autor } from './autores.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap, delay, take } from 'rxjs/operators';

@Injectable()
export class AutoresService {

  private readonly API = `${environment.API}Autor`;
  constructor(private http: HttpClient) { }

  list() {
    console.log(this.API);
    return this.http.get<Autor[]>(this.API)
      .pipe(
        delay(2000),
        tap(console.log)
      );
  }

  get() {
    return this.http.get<Autor[]>(this.API)
      .pipe(        
        tap(console.log)
      );
  }

  getAutor(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.get<Autor>(_URL)
      .pipe(
        tap(console.log)
      );
  }

  create(request: Autor) {
    return this.http.post<Autor>(this.API, request).pipe(take(1));
  }  

  update(Id: string, request: Autor) {
    const _URL = `${this.API}/${Id}`;
    return this.http.put<Autor>(_URL, request).pipe(take(1));
  }

  delete(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.delete<any>(_URL).pipe(take(1));
  }
}
