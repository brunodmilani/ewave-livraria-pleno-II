import { Component, OnInit, TemplateRef } from '@angular/core';
import { Livro } from './livros.model';
import { LivrosService } from './livros.service';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Observable, Subject } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { empty } from 'rxjs/observable/empty';

@Component({
  selector: 'app-livros',
  templateUrl: './livros.component.html'
})
export class LivrosComponent implements OnInit {

  id: string;
  modalRef: BsModalRef;
  livros$: Observable<Livro[]>;
  error$ = new Subject<boolean>();
  item: string;

  constructor(
    private service: LivrosService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    public router: Router)
  { }
    
  ngOnInit() {
    this.refresh();
  }

  openModal(template: TemplateRef<any>, capaPath: string) {
    this.item = capaPath;
    this.modalRef = this.modalService.show(template, { class: 'modal-md' });
  }

  openModalDelete(template: TemplateRef<any>, id: string) {
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
    this.livros$ = this.service.get()
      .pipe(
        catchError(error => {
          this.error$.next(true);
          this.toastr.error('Erro ao Carregar. Tente novamente mais tarde.');
          return empty();
        })
      );
  }
}
