import { AutoresService } from './autores.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-autores',
  templateUrl: './autores.component.html'
})
export class AutoresComponent implements OnInit {

  constructor(private AutoresService) { }

  ngOnInit() {
  }
}
