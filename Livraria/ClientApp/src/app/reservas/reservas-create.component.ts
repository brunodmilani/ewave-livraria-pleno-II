import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ReservasService } from './reservas.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { error } from 'protractor';
import { UsuariosService } from '../usuarios/usuarios.service';
import { LivrosService } from '../livros/livros.service';
import { Usuario } from '../usuarios/usuarios.model';
import { Livro } from '../livros/livros.model';

@Component({
  selector: 'app-reservas-create',
  templateUrl: './reservas-create.component.html'
})
export class ReservasCreateComponent implements OnInit {

  form: FormGroup;
  submitted = false;
  usuarios: Usuario[];
  livros: Livro[];

  constructor(
    private service: ReservasService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private serviceUsuarios: UsuariosService,
    private serviceLivros: LivrosService,
    public router: Router) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      usuarioId: [null, Validators.required],
      livroId: [null, Validators.required]
    });

    this.serviceUsuarios.get()
      .subscribe(res =>
        this.usuarios = res
      );

    this.serviceLivros.get()
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
          error => this.toastr.error('Ocorreu um erro: ' + error.message),
          () => {
            this.submitted = false;
            this.form.reset();
            this.router.navigateByUrl('/reservas');
          }
        );
    }
  }
}
