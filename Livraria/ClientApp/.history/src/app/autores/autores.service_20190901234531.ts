import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class AutoresService {

  private URL = 'http://localhost:3547/api/Autores/';

  constructor(private http: HttpClient) { }

  getAutores() {

  }
}
