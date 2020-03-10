import { Component, OnInit } from '@angular/core';
import { UsuariosService } from './usuarios.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Instituicao } from '../instituicoes/instituicoes.model';
import { InstituicoesService } from '../instituicoes/instituicoes.service';

@Component({
  selector: 'app-usuarios-edit',
  templateUrl: './usuarios-edit.component.html'
})
export class UsuariosEditComponent implements OnInit {

  id: string;
  form: FormGroup;
  submitted = false;
  instituicoes: Instituicao[];

  constructor(
    private service: UsuariosService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private serviceInstituicoes: InstituicoesService,
    public router: Router,
    private route: ActivatedRoute)
  { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.form = this.formBuilder.group({
      id: [null],
      nome: [null, Validators.required],
      endereco: [null, Validators.required],
      cpf: [null, Validators.required],
      telefone: [null],
      instituicaoId: [null],
      email: [null]
    });

    this.service.getUsuario(this.id).subscribe(res => {
      this.form.patchValue({
        id: res.id,
        nome: res.nome,
        endereco: res.endereco,
        cpf: res.cpf,
        telefone: res.telefone,
        instituicaoId: res.instituicaoId,
        email: res.email
      });
    });

    this.serviceInstituicoes.get()
      .subscribe(res =>
        this.instituicoes = res
      );    
  }

  hasError(field: string) {
    return this.form.get(field).errors;
  }

  alterar() {
    this.submitted = true;
    if (this.form.valid) {
      this.service.update(this.id, this.form.value)
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
}
