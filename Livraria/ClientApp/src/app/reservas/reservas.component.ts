import { Reserva } from './reservas.model';
import { ReservasService } from './reservas.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Observable, Subject } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { empty } from 'rxjs/observable/empty';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reservas',
  templateUrl: './reservas.component.html'
})
export class ReservasComponent implements OnInit {

  id: string;
  submitted = false;
  modalRef: BsModalRef;
  reservas$: Observable<Reserva[]>;
  error$ = new Subject<boolean>();

  constructor(
    private service: ReservasService,
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
    this.reservas$ = this.service.list()
      .pipe(
        catchError(error => {
          this.error$.next(true);
          this.toastr.error('Erro ao Carregar. Tente novamente mais tarde.');
          return empty();
        })
      );
  }

  realizarEmprestimo(id) {
    this.submitted = true;
    this.service.emprestimo(id)
      .subscribe(
        success => this.toastr.success('Empréstimo realizado com sucesso!'),
        error => error.error ? this.toastr.warning(error.error) : this.toastr.error('Ocorreu um erro: ' + error.message),
        () => {
          this.submitted = false;
          this.router.navigateByUrl('/emprestimos');
        }
      );
  }
}
