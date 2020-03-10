import { Component, OnInit, TemplateRef } from '@angular/core';
import { Genero } from './generos.model';
import { GenerosService } from './generos.service';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable, Subject } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { empty } from 'rxjs/observable/empty';

@Component({
  selector: 'app-generos',
  templateUrl: './generos.component.html'
})
export class GenerosComponent implements OnInit {

  id: string;
  modalRef: BsModalRef;
  generos$: Observable<Genero[]>;
  error$ = new Subject<boolean>();

  constructor(
    private service: GenerosService,
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
    this.generos$ = this.service.list()
      .pipe(
        catchError(error => {
          this.error$.next(true);
          this.toastr.error('Erro ao Carregar. Tente novamente mais tarde.');
          return empty();
        })
      );
  }
}
