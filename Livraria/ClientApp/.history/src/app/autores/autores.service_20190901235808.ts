import { Autor } from './autores.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AutoresService {

  private URL = 'http://localhost:3547/api/Autores/';

  constructor(private http: HttpClient) { }

  getAutores() Observable<Autor[]> {
    return this.http.get<Autor[]>(this.URL);
  }
}
