import { Component } from '@angular/core';
import { InstituicoesService } from './instituicoes.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-instituicoes-edit',
  templateUrl: './instituicoes-edit.component.html'
})
export class InstituicoesEditComponent {

  id: string;
  form: FormGroup;
  submitted = false;

  constructor(
    private service: InstituicoesService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    public router: Router,
    private route: ActivatedRoute)
  { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.form = this.formBuilder.group({
      id: [null],
      nome: [null, Validators.required],
      endereco: [null, Validators.required],
      cnpj: [null, Validators.required],
      telefone: [null, Validators.required]
    });

    this.service.getInstituicao(this.id).subscribe(res => {
      this.form.patchValue({
        id: res.id,
        nome: res.nome,
        endereco: res.endereco,
        cnpj: res.cnpj,
        telefone: res.telefone
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
            this.router.navigateByUrl('/instituicoes');
          }
        );
    }
  }
}
