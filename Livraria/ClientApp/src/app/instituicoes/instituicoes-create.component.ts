import { Component, OnInit } from '@angular/core';
import { InstituicoesService } from './instituicoes.service';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-instituicoes-create',
  templateUrl: './instituicoes-create.component.html'
})
export class InstituicoesCreateComponent implements OnInit {

  form: FormGroup;
  submitted = false;

  constructor(
    private service: InstituicoesService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    public router: Router)
  { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      nome: [null, Validators.required],
      endereco: [null, Validators.required],
      cnpj: [null, Validators.required],
      telefone: [null, Validators.required]
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
            this.router.navigateByUrl('/instituicoes');
          }
        );
    }
  }
}
