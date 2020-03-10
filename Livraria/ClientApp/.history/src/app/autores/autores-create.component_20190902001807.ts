import { ActivatedRoute } from '@angular/router';
import { Autor } from './autores.model';
import { Component, OnInit } from '@angular/core';
import { AutoresService } from './autores.service';

@Component({
  selector: 'app-autores-create',
  templateUrl: './autores-create.component.html'
})
export class AutoresCreateComponent implements OnInit {

  private router = ActivatedRoute;

  request: Autor = {
    Id: 0,
    Nome: ''
  };

  constructor(private autoresService: AutoresService,
    r: ActivatedRoute) {
      this.router = r;
     }

  ngOnInit() { }

  save() {
    this.autoresService.create(this.request)
    .subscribe(
      this.router.navigate(['/heroes']);
    );
  }
}
