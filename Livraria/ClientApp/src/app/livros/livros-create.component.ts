import { Component, Inject } from '@angular/core';
import { LivrosService } from './livros.service';
import { Router } from '@angular/router';
import { Autor } from '../autores/autores.model';
import { AutoresService } from '../autores/autores.service';
import { Genero } from '../generos/generos.model';
import { GenerosService } from '../generos/generos.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-livros-create',
  templateUrl: './livros-create.component.html'
})
export class LivrosCreateComponent {

  capa: File = null;
  form: FormGroup;
  submitted = false;
  autores: Autor[];
  generos: Genero[];

  constructor(
    private service: LivrosService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private serviceAutores: AutoresService,
    private serviceGeneros: GenerosService,
    public router: Router)
  { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      titulo: [null, Validators.required],
      generoId: [null, Validators.required],
      autorId: [null, Validators.required],
      capaPath: [null, Validators.required],
      sinopse: [null]
    });

    this.serviceAutores.get()
      .subscribe(res =>
        this.autores = res
    );

    this.serviceGeneros.get()
      .subscribe(res =>
        this.generos = res
    );
  }

  selectedFile(event) {
    this.capa = event.target.files[0];
  }

  hasError(field: string) {
    return this.form.get(field).errors;
  }

  save() {
    this.submitted = true;
    if (this.form.valid) {
      this.service.create(this.form.value)
        .subscribe( res => {
          this.toastr.success('Salvo com sucesso!');
          this.service.upload(this.capa, res.id)
            .subscribe(
              success => this.toastr.success('Upload da capa feito com sucesso!'),
              error => this.toastr.error('Ocorreu um erro: ' + error.message),
              () => {
                this.submitted = false;
                this.form.reset();
                this.router.navigateByUrl('/livros');
              }
            );
          },
          error => this.toastr.error('Ocorreu um erro: ' + error.message),
        );
    }
  }
}
