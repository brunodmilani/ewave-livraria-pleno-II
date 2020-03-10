import { Autor } from './autores.model';
import { AutoresService } from './autores.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-autores',
  templateUrl: './autores.component.html'
})
export class AutoresComponent implements OnInit {

  autores: Autor;

  constructor(private autoresService: AutoresService) { }

  ngOnInit() {
    this.autoresService.getAutores().subscribe(

    )
  }
}
