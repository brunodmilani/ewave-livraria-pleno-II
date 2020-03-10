import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxMaskModule, IConfig } from 'ngx-mask';

import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AutoresComponent } from './autores/autores.component';
import { AutoresCreateComponent } from './autores/autores-create.component';
import { AutoresEditComponent } from './autores/autores-edit.component';
import { GenerosComponent } from './generos/generos.component';
import { GenerosCreateComponent } from './generos/generos-create.component';
import { GenerosEditComponent } from './generos/generos-edit.component';
import { InstituicoesComponent } from './instituicoes/instituicoes.component';
import { InstituicoesCreateComponent } from './instituicoes/instituicoes-create.component';
import { InstituicoesEditComponent } from './instituicoes/instituicoes-edit.component';
import { LivrosComponent } from './livros/livros.component';
import { LivrosCreateComponent } from './livros/livros-create.component';
import { LivrosEditComponent } from './livros/livros-edit.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { UsuariosCreateComponent } from './usuarios/usuarios-create.component';
import { UsuariosEditComponent } from './usuarios/usuarios-edit.component';
import { AutoresService } from './autores/autores.service';
import { InstituicoesService } from './instituicoes/instituicoes.service';
import { GenerosService } from './generos/generos.service';
import { LivrosService } from './livros/livros.service';
import { UsuariosService } from './usuarios/usuarios.service';
import { EmprestimosComponent } from './emprestimos/emprestimos.component';
import { EmprestimosCreateComponent } from './emprestimos/emprestimos-create.component';
import { ReservasComponent } from './reservas/reservas.component';
import { ReservasCreateComponent } from './reservas/reservas-create.component';
import { EmprestimosService } from './emprestimos/emprestimos.service';
import { ReservasService } from './reservas/reservas.service';

export let options: Partial<IConfig> | (() => Partial<IConfig>);

@NgModule({
  declarations: [
    AppComponent,    
    NavMenuComponent,
    HomeComponent,
    AutoresComponent,
    AutoresCreateComponent,
    AutoresEditComponent,
    InstituicoesComponent,
    InstituicoesCreateComponent,
    InstituicoesEditComponent,
    GenerosComponent,
    GenerosCreateComponent,
    GenerosEditComponent,
    LivrosComponent,
    LivrosCreateComponent,
    LivrosEditComponent,
    UsuariosComponent,
    UsuariosCreateComponent,
    UsuariosEditComponent,
    EmprestimosComponent,
    EmprestimosCreateComponent,
    ReservasComponent,
    ReservasCreateComponent
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgxMaskModule.forRoot(options),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),    
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'autores', component: AutoresComponent },
      { path: 'autores/create', component: AutoresCreateComponent },
      { path: 'autores/edit/:id', component: AutoresEditComponent },
      { path: 'instituicoes', component: InstituicoesComponent },
      { path: 'instituicoes/create', component: InstituicoesCreateComponent },
      { path: 'instituicoes/edit/:id', component: InstituicoesEditComponent },
      { path: 'generos', component: GenerosComponent },
      { path: 'generos/create', component: GenerosCreateComponent },
      { path: 'generos/edit/:id', component: GenerosEditComponent },
      { path: 'livros', component: LivrosComponent },
      { path: 'livros/create', component: LivrosCreateComponent },
      { path: 'livros/edit/:id', component: LivrosEditComponent },
      { path: 'usuarios', component: UsuariosComponent },
      { path: 'usuarios/create', component: UsuariosCreateComponent },
      { path: 'usuarios/edit/:id', component: UsuariosEditComponent },
      { path: 'emprestimos', component: EmprestimosComponent },
      { path: 'emprestimos/create', component: EmprestimosCreateComponent },
      { path: 'reservas', component: ReservasComponent },
      { path: 'reservas/create', component: ReservasCreateComponent },
    ])
  ],
  providers: [
    AutoresService,
    InstituicoesService,
    GenerosService,
    LivrosService,
    UsuariosService,
    EmprestimosService,
    ReservasService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
