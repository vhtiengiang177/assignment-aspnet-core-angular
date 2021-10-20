import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './modules/layout/nav-menu/nav-menu.component';
import { LoginComponent } from './modules/authentication/login/login.component';
import { HomeComponent } from './modules/layout/home/home.component';
import { ProductsListComponent } from './modules/products/products-list/products-list.component';
import { RouterModule } from '@angular/router';
import { NoAccessComponent } from './_shared/components/no-access/no-access.component';
import { NotFoundComponent } from './_shared/components/not-found/not-found.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthGuard } from './services/auth-guard/auth-guard.service';
import { ActivatedLogin } from './services/activated-login/activated-login.service';
import { AuthService } from './services/auth/auth.service';
import { SearchProductComponent } from './modules/products/search-product/search-product.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { MatDialogModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatNativeDateModule } from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AppErrorHandling } from './_shared/errors/app-error-handling';
import { DateValueAccessorModule } from 'angular-date-value-accessor';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { FormProductComponent } from './modules/products/form-product/form-product.component';
import { DetailProductComponent } from './modules/products/detail-product/detail-product.component';
import { DeleteFormComponent } from './modules/products/delete-form/delete-form.component';

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
    MatDatepickerModule,
    MatNativeDateModule,
    FontAwesomeModule,
    DateValueAccessorModule,
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
        component: DetailProductComponent,
        canActivate: [AuthGuard]
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
