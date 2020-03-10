import { Emprestimo } from './emprestimos.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap, delay, take } from 'rxjs/operators';

@Injectable()
export class EmprestimosService {

  private readonly API = `${environment.API}Emprestimo`;
  constructor(private http: HttpClient) { }

  list() {
    console.log(this.API);
    return this.http.get<Emprestimo[]>(this.API)
      .pipe(
        delay(2000),
        tap(console.log)
      );
  }

  get() {
    return this.http.get<Emprestimo[]>(this.API)
      .pipe(        
        tap(console.log)
      );
  }

  getEmprestimo(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.get<Emprestimo>(_URL)
      .pipe(
        tap(console.log)
      );
  }

  create(request: Emprestimo) {
    return this.http.post<Emprestimo>(this.API, request).pipe(take(1));
  }  

  update(Id: string, request: Emprestimo) {
    const _URL = `${this.API}/${Id}`;
    return this.http.put<Emprestimo>(_URL, request).pipe(take(1));
  }

  delete(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.delete<any>(_URL).pipe(take(1));
  }

  devolucao(Id: number) {
    const _URL = `${this.API}/${Id}/devolucao`;
    return this.http.put<any>(_URL, Id).pipe(take(1));
  }
}
