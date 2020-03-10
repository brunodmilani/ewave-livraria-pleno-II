import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AutoresComponent } from './autores/autores.component';
import { AutoresCreateComponent } from './autores/autores-create.component';
import { AutoresEditComponent } from './autores/autores-edit.component';
import { GenerosComponent } from './generos/generos.component';
import { GenerosCreateComponent } from './generos/generos-create.component';
import { GenerosEditComponent } from './generos/generos-edit.component';
import { EditorasComponent } from './editoras/editoras.component';
import { EditorasCreateComponent } from './editoras/editoras-create.component';
import { EditorasEditComponent } from './editoras/editoras-edit.component';
import { LivrosComponent } from './livros/livros.component';
import { LivrosCreateComponent } from './livros/livros-create.component';
import { LivrosEditComponent } from './livros/livros-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    HttpClientModule,
    NavMenuComponent,
    HomeComponent,
    AutoresComponent,
    AutoresCreateComponent,
    AutoresEditComponent,
    EditorasComponent,
    EditorasCreateComponent,
    EditorasEditComponent,
    GenerosComponent,
    GenerosCreateComponent,
    GenerosEditComponent,
    LivrosComponent,
    LivrosCreateComponent,
    LivrosEditComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'autores', component: AutoresComponent },
      { path: 'autores/create', component: AutoresCreateComponent },
      { path: 'autores/edit/:id', component: AutoresEditComponent },
      { path: 'editoras', component: EditorasComponent },
      { path: 'editoras/create', component: EditorasCreateComponent },
      { path: 'editoras/edit/:id', component: EditorasEditComponent },
      { path: 'generos', component: GenerosComponent },
      { path: 'generos/create', component: GenerosCreateComponent },
      { path: 'generos/edit/:id', component: GenerosEditComponent },
      { path: 'livros', component: LivrosComponent },
      { path: 'livros/create', component: LivrosCreateComponent },
      { path: 'livros/edit/:id', component: LivrosEditComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
