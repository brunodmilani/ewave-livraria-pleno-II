import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { EmprestimosService } from './emprestimos.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { error } from 'protractor';
import { UsuariosService } from '../usuarios/usuarios.service';
import { Usuario } from '../usuarios/usuarios.model';
import { Livro } from '../livros/livros.model';
import { LivrosService } from '../livros/livros.service';

@Component({
  selector: 'app-emprestimos-create',
  templateUrl: './emprestimos-create.component.html'
})
export class EmprestimosCreateComponent implements OnInit {

  form: FormGroup;
  submitted = false;
  usuarios: Usuario[];
  livros: Livro[];

  constructor(
    private service: EmprestimosService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private serviceUsuarios: UsuariosService,
    private serviceLivros: LivrosService,
    public router: Router) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      usuarioId: [null, Validators.required],
      livroId: [null, Validators.required],
      data: [null, Validators.required]
    });

    this.serviceUsuarios.get()
      .subscribe(res =>
        this.usuarios = res
    );

    this.serviceLivros.getNotEmprestado()
      .subscribe(res =>
        this.livros = res
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
          error => error.error ? this.toastr.warning(error.error) : this.toastr.error('Ocorreu um erro: ' + error.message),
          () => {
            this.submitted = false;
            this.form.reset();
            this.router.navigateByUrl('/emprestimos');
          }
        );
    }
  }
}
