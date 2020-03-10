import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AutoresService } from './autores.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { error } from 'protractor';

@Component({
  selector: 'app-autores-create',
  templateUrl: './autores-create.component.html'
})
export class AutoresCreateComponent implements OnInit {

  form: FormGroup;
  submitted = false;

  constructor(
    private service: AutoresService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    public router: Router) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      nome: [null, Validators.required]
    });
  }

  hasError(field: string) {
    return this.form.get(field).errors;
  }

  save() {
    this.submitted = true;
    if (this.form.valid) {      
      this.service.create(this.form.value)
        .subscribe(
          success => this.toastr.success('Salvo com sucesso!'),
          error => this.toastr.error('Ocorreu um erro: ' + error.message),
          () => {
            this.submitted = false;
            this.form.reset();
            this.router.navigateByUrl('/autores');
          }
        );
    }
  }
}
