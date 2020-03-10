import { Component, OnInit, TemplateRef } from '@angular/core';
import { Instituicao } from './instituicoes.model';
import { InstituicoesService } from './instituicoes.service';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable, Subject } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { empty } from 'rxjs/observable/empty';

@Component({
  selector: 'app-instituicoes',
  templateUrl: './instituicoes.component.html'
})
export class InstituicoesComponent implements OnInit {

  id: string;
  modalRef: BsModalRef;
  instituicoes$: Observable<Instituicao[]>;
  error$ = new Subject<boolean>();

  constructor(
    private service: InstituicoesService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    public router: Router)
  { }

  ngOnInit() {
    this.refresh();
  }

  openModal(template: TemplateRef<any>, id: string) {
    this.id = id;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.service.delete(this.id)
      .subscribe(
        success => this.toastr.success('Excluído com sucesso!'),
        error => this.toastr.error('Ocorreu um erro: ' + error.message),
        () => {
          this.refresh();
        }
      );

    this.modalRef.hide();
  }

  decline(): void {
    this.modalRef.hide();
  }

  refresh() {
    this.instituicoes$ = this.service.list()
      .pipe(
        catchError(error => {
          this.error$.next(true);
          this.toastr.error('Erro ao Carregar. Tente novamente mais tarde.');
          return empty();
        })
      );
  }
}
