import { Component } from '@angular/core';
import { LivrosService } from './livros.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Autor } from '../autores/autores.model';
import { Genero } from '../generos/generos.model';
import { AutoresService } from '../autores/autores.service';
import { GenerosService } from '../generos/generos.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-livros-edit',
  templateUrl: './livros-edit.component.html'
})
export class LivrosEditComponent {

  capa: File = null;
  form: FormGroup;
  submitted = false;
  id: string;
  autores: Autor[];
  generos: Genero[];

  constructor(
    private service: LivrosService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private serviceAutores: AutoresService,
    private serviceGeneros: GenerosService,
    public router: Router,
    private route: ActivatedRoute)
  { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.form = this.formBuilder.group({
      id: [null],
      titulo: [null, Validators.required],
      generoId: [null, Validators.required],
      autorId: [null, Validators.required],
      capaPath: [null],
      sinopse: [null]
    });

    this.service.getLivro(this.id).subscribe(res => {
      this.form.patchValue({
        id: res.id,
        titulo: res.titulo,
        generoId: res.generoId,
        autorId: res.autorId,
        sinopse: res.sinopse,
        capaPath: ''
      });
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

  alterar() {
    this.submitted = true;
    if (this.form.valid) {
      this.service.update(this.id, this.form.value)
        .subscribe(res => {
          this.toastr.success('Salvo com sucesso!');
          if (this.capa) {
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
          }
        });
    }
  }
}
