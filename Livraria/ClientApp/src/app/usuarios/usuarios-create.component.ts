import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { UsuariosService } from './usuarios.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Validation } from './validation';
import { ToastrService } from 'ngx-toastr';
import { Instituicao } from '../instituicoes/instituicoes.model';
import { InstituicoesService } from '../instituicoes/instituicoes.service';


@Component({
  selector: 'app-usuarios-create',
  templateUrl: './usuarios-create.component.html'
})
export class UsuariosCreateComponent implements OnInit {

  form: FormGroup;
  submitted = false;
  instituicoes: Instituicao[];

  constructor(
    private service: UsuariosService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private serviceInstituicoes: InstituicoesService,
    public router: Router) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      nome: ['', Validators.required],
      endereco: ['', Validators.required],
      cpf: ['', Validators.compose([Validators.required])],
      telefone: ['', Validators.compose([Validators.required])],
      instituicaoId: [''],
      email: ['']
    });

    this.serviceInstituicoes.get()
      .subscribe(res =>
        this.instituicoes = res
      );
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
            this.router.navigateByUrl('/usuarios');
          }
        );
    }
  }

  get email() {
    return this.form.get('email');
  }

  get telefone() {
    return this.form.get('telefone');
  }
}
