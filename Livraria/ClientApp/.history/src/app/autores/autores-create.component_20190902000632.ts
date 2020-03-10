import { Autor } from './autores.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-autores-create',
  templateUrl: './autores-create.component.html'
})
export class AutoresCreateComponent implements OnInit {

  request: Autor = {
    Nome: '';
  };

  constructor() { }

  ngOnInit() {

  }
}
