import { Livro } from './livros.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap, delay, take } from 'rxjs/operators';

@Injectable()
export class LivrosService {

  private readonly API = `${environment.API}Livro`;
  constructor(private http: HttpClient) { }

  get() {
    return this.http.get<Livro[]>(this.API)
      .pipe(
        delay(2000),
        tap(console.log)
      );
  }

  getNotEmprestado() {
    return this.http.get<Livro[]>(`${this.API}/NotEmprestado`)
      .pipe(
        delay(2000),
        tap(console.log)
      );
  }

  getLivro(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.get<Livro>(_URL)
      .pipe(
        tap(console.log)
      );
  }

  create(request: Livro) {
    return this.http.post<Livro>(this.API, request).pipe(take(1));
  }

  upload(capa: File, id: any) {
    const _URL = `${this.API}/Upload/${id}`;
    const formData = new FormData();
    formData.append('capa', capa, capa.name);
    return this.http.post(_URL, formData).pipe(take(1));
  }

  update(Id: string, request: Livro) {
    const _URL = `${this.API}/${Id}`;
    return this.http.put<Livro>(_URL, request).pipe(take(1));
  }

  delete(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.delete<any>(_URL).pipe(take(1));
  }
}
