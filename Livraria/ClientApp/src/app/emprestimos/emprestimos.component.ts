import { Emprestimo } from './emprestimos.model';
import { EmprestimosService } from './emprestimos.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Observable, Subject } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { empty } from 'rxjs/observable/empty';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-emprestimos',
  templateUrl: './emprestimos.component.html'
})
export class EmprestimosComponent implements OnInit {

  id: string;
  submitted = false;
  modalRef: BsModalRef;
  emprestimos$: Observable<Emprestimo[]>;
  error$ = new Subject<boolean>();

  constructor(
    private service: EmprestimosService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    public router: Router) { }

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
    this.emprestimos$ = this.service.list()
      .pipe(
        catchError(error => {
          this.error$.next(true);
          this.toastr.error('Erro ao Carregar. Tente novamente mais tarde.');
          return empty();
        })
      );
  }

  realizarDevolucao(id) {
    this.submitted = true;
    this.service.devolucao(id)
      .subscribe(
        success => this.toastr.success('Livro devolvido com sucesso!'),
        error => error.error ? this.toastr.warning(error.error) : this.toastr.error('Ocorreu um erro: ' + error.message),
        () => {
          this.submitted = false;
          this.refresh();
        }
      );
  }
}
