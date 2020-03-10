import { Autor } from './autores.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';

@Injectable()
export class AutoresService {
  private URL;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
     this.URL = baseUrl + 'api/Autores/';
   }

  getAutores() {
    return this.http.get<Autor[]>(this.URL);
  }

  create(request: Autor) {
    return this.http.post<Autor>(this.URL, request);
  }
}
