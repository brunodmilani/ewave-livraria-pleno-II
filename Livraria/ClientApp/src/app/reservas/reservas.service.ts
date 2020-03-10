import { Reserva } from './reservas.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap, delay, take } from 'rxjs/operators';

@Injectable()
export class ReservasService {

  private readonly API = `${environment.API}Reserva`;
  constructor(private http: HttpClient) { }

  list() {
    return this.http.get<Reserva[]>(this.API)
      .pipe(
        delay(2000),
        tap(console.log)
      );
  }

  get() {
    return this.http.get<Reserva[]>(this.API)
      .pipe(        
        tap(console.log)
      );
  }

  getReserva(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.get<Reserva>(_URL)
      .pipe(
        tap(console.log)
      );
  }

  create(request: Reserva) {
    return this.http.post<Reserva>(this.API, request).pipe(take(1));
  }  

  update(Id: string, request: Reserva) {
    const _URL = `${this.API}/${Id}`;
    return this.http.put<Reserva>(_URL, request).pipe(take(1));
  }

  delete(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.delete<any>(_URL).pipe(take(1));
  }

  emprestimo(Id: number) {
    const _URL = `${this.API}/${Id}/emprestimo`;
    return this.http.post<any>(_URL, Id).pipe(take(1));
  }
}
