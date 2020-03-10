import { Autor } from './autores.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-autores-create',
  templateUrl: './autores-create.component.html'
})
export class AutoresCreateComponent implements OnInit {

  request: Autor = {
    Id: 0,
    Nome: ''
  };

  constructor() { }

  ngOnInit() {

  }
}
