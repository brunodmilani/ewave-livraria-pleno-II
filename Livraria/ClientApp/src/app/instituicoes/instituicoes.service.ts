import { Instituicao } from './instituicoes.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap, delay, take } from 'rxjs/operators';

@Injectable()
export class InstituicoesService {

  private readonly API = `${environment.API}Instituicao`;
  constructor(private http: HttpClient) { }

  list() {
    console.log(this.API);
    return this.http.get<Instituicao[]>(this.API)
      .pipe(
        delay(2000),
        tap(console.log)
      );
  }

  get() {
    return this.http.get<Instituicao[]>(this.API)
      .pipe(
        tap(console.log)
      );
  }

  getInstituicao(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.get<Instituicao>(_URL)
      .pipe(
        tap(console.log)
      );
  }

  create(request: Instituicao) {
    return this.http.post<Instituicao>(this.API, request).pipe(take(1));
  }

  update(Id: string, request: Instituicao) {
    const _URL = `${this.API}/${Id}`;
    return this.http.put<Instituicao>(_URL, request).pipe(take(1));
  }

  delete(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.delete<any>(_URL).pipe(take(1));
  }
}
