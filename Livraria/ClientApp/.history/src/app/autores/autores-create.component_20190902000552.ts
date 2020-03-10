import { Autor } from './autores.model';
import { Component } from '@angular/core';

@Component({
  selector: 'app-autores-create',
  templateUrl: './autores-create.component.html'
})
export class AutoresCreateComponent {

  request: Autor = {
    Nome: '';
  }

  constructor() {
  }
}
