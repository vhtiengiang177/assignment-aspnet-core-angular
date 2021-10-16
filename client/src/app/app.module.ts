import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu/nav-menu.component';
import { LoginComponent } from './login/login/login.component';
import { HomeComponent } from './home/home/home.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { RouterModule } from '@angular/router';
import { NoAccessComponent } from './no-access/no-access/no-access.component';
import { NotFoundComponent } from './not-found/not-found/not-found.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthGuard } from './services/auth-guard/auth-guard.service';
import { ActivatedLogin } from './services/activated-login/activated-login.service';
import { AuthService } from './services/auth/auth.service';
import { SearchProductComponent } from './search-product/search-product/search-product.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { MatDialogModule, MatFormFieldModule, MatInputModule, MatSelectModule } from '@angular/material';
import { FormProductComponent } from './form-product/form-product/form-product.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AppErrorHandling } from './common/app-error-handling';
import { DeleteProductComponent } from './delete-product/delete-product/delete-product.component';
import { DetailProductComponent } from './detail-product/detail-product/detail-product.component';
import { DeleteFormComponent } from './products/delete-form/delete-form/delete-form.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    HomeComponent,
    ProductsListComponent,
    NoAccessComponent,
    NotFoundComponent,
    SearchProductComponent,
    FormProductComponent,
    DeleteProductComponent,
    DetailProductComponent,
    DeleteFormComponent
  ],
  entryComponents: [
    FormProductComponent,
    DeleteFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatPaginatorModule,
    MatSelectModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    FontAwesomeModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { 
        path: 'login', 
        component: LoginComponent,
        canActivate: [ActivatedLogin]
      },
      { path: 'products-list', 
        component: ProductsListComponent, 
        canActivate: [AuthGuard]
      },
      { path: 'no-access', component: NoAccessComponent },
      { path: 'not-found', component: NotFoundComponent },
      {
        path: 'products/:id',
        component: DetailProductComponent
      }
    ]),
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    AuthService,
    { provide: ErrorHandler, useClass: AppErrorHandling }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
