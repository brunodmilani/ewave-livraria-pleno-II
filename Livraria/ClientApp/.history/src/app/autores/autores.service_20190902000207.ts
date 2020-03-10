import { Autor } from './autores.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class AutoresService {

  string baseUrl = @Inject('BASE_URL');
  private URL = 'http://localhost:3547/api/Autores/';

  constructor(private http: HttpClient) { }

  getAutores() {
    return this.http.get<Autor[]>(this.URL);
  }

  create(request: Autor) {
    return this.http.post<Autor>(this.URL, request);
  }
}
