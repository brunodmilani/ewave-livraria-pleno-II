import { Usuario } from './usuarios.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap, delay, take } from 'rxjs/operators';

@Injectable()
export class UsuariosService {

  private readonly API = `${environment.API}Usuario`;
  constructor(private http: HttpClient) { }

  list() {
    console.log(this.API);
    return this.http.get<Usuario[]>(this.API)
      .pipe(
        delay(2000),
        tap(console.log)
      );
  }

  get() {
    return this.http.get<Usuario[]>(this.API)
      .pipe(        
        tap(console.log)
      );
  }

  getUsuario(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.get<Usuario>(_URL)
      .pipe(
        tap(console.log)
      );
  }

  create(request: Usuario) {
    return this.http.post<Usuario>(this.API, request).pipe(take(1));
  }  

  update(Id: string, request: Usuario) {
    const _URL = `${this.API}/${Id}`;
    return this.http.put<Usuario>(_URL, request).pipe(take(1));
  }

  delete(Id: string) {
    const _URL = `${this.API}/${Id}`;
    return this.http.delete<any>(_URL).pipe(take(1));
  }
}
