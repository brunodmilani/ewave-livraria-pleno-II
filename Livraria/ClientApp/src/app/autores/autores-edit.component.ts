import { Component, OnInit } from '@angular/core';
import { AutoresService } from './autores.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-autores-edit',
  templateUrl: './autores-edit.component.html'
})
export class AutoresEditComponent implements OnInit {

  id: string;
  form: FormGroup;
  submitted = false;

  constructor(
    private service: AutoresService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    public router: Router,
    private route: ActivatedRoute)
  { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.form = this.formBuilder.group({
      id: [null],
      nome: [null, Validators.required]
    });

    this.service.getAutor(this.id)
      .subscribe(res => {
        this.form.patchValue({
          id: res.id,
          nome: res.nome
        });
    });    
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
            this.router.navigateByUrl('/autores');
          }
        );
    }
  }
}
