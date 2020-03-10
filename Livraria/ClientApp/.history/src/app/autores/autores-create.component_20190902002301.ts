import { Router } from '@angular/router';
import { Autor } from './autores.model';
import { Component, OnInit } from '@angular/core';
import { AutoresService } from './autores.service';

@Component({
  selector: 'app-autores-create',
  templateUrl: './autores-create.component.html'
})
export class AutoresCreateComponent implements OnInit {

  request: Autor = {
    Id: 0,
    Nome: ''
  };

  constructor(private autoresService: AutoresService, private route: ActivatedRoute) { }

  ngOnInit() { }

  save() {
    this.autoresService.create(this.request)
    .subscribe(
      this.route.navigateByUrl(['/autores']);
    );
  }
}
