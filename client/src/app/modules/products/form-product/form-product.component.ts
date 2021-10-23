import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MatOption, MatSelect, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { CategoriesStoreService } from 'src/app/services/store/categories-store/categories-store.service';
import { Category } from 'src/app/services/model/category.model';
import { FormProduct } from 'src/app/services/model/form-product.model';
import { ProductsStoreService } from 'src/app/services/store/products-store/products-store.service';
import { SuppliersStoreService } from 'src/app/services/store/suppliers-store/suppliers-store.service';
import { BadRequestError } from 'src/app/_shared/errors/bad-request';
import { AppError } from 'src/app/_shared/errors/app-error';

@Component({
  selector: 'form-product',
  templateUrl: './form-product.component.html',
  styleUrls: ['./form-product.component.css']
})
export class FormProductComponent implements OnInit {
  @ViewChild('selectCategory', {static: false}) selectCategory: MatSelect;
  @ViewChild('selectSupplier', {static:false}) selectSupplier: MatSelect;
  isAll : boolean = false;
  categories: Category[] = [];
  ratingOption: number[] = [];
  newId: number;
  title: string;
  
  constructor(public dialogRef: MatDialogRef<FormProductComponent>,
    @Inject(MAT_DIALOG_DATA) public data: FormProduct,
    private categoriesStore: CategoriesStoreService,
    private suppliersStore: SuppliersStoreService,
    private productsStore: ProductsStoreService,
    private toastr: ToastrService) {
      this.ratingOption = [1,2,3,4,5];

      if(this.data.categories.length == this.categoriesStore.categories.length) {
        this.data.categories.unshift({id: 0, name: 'All'});
        this.isAll = !this.isAll;
      }
  
      if(!this.data.product.releaseDate) {
        this.data.product.releaseDate = new Date();
      }
  
      if(!this.data.product.discontinuedDate) {
        this.data.product.discontinuedDate = new Date();
        this.data.product.discontinuedDate.setDate(this.data.product.discontinuedDate.getDate() + 1);
      }
  
      if(!this.data.product.rating) {
        this.data.product.rating = 5;
      }
    }

  ngOnInit() {

  }

  save() {
    if(!this.data.product.name || !this.data.product.rating || !this.data.product.price ) {
      this.toastr.error("Please fill in all the required fields.");
    }
    else {
      let idCategories: number[] = [];
      this.data.categories.filter(k=>k.id != 0).forEach(element => {
        idCategories.push(element.id)
      });
      if (this.data.typeform === 0) {
        this.productsStore.add(this.data.product, idCategories).subscribe(res => {
          this.dialogRef.close(res);
        })
      }
      else if (this.data.typeform === 1) { 
        this.productsStore.edit(this.data.product, idCategories)
        .subscribe(() => {
        this.dialogRef.close(this.data.product);
          this.toastr.success("The product #" + this.data.product.id + " has been edited")
        }, (error : AppError) => {
          if(error instanceof BadRequestError)
            this.toastr.error("Edit product #" + this.data.product.id + " failed")
          else throw error
        })
      }
      else this.toastr.warning("Invalid");
    }
  }

  checkReleaseDate() {
    if(this.data.product.releaseDate >= this.data.product.discontinuedDate) {
      this.data.product.releaseDate.setDate(this.data.product.discontinuedDate.getDate() - 1);
    }
  }

  checkDiscontinuedDate() {
    if(this.data.product.releaseDate >= this.data.product.discontinuedDate) {
      this.data.product.discontinuedDate.setDate(this.data.product.releaseDate.getDate() + 1);
    }
  }

  checkPrice() {
    if(this.data.product.price < 1000) {
      this.data.product.price = 1000;
    }
  }

  selectedSupplier(selected) {
    this.data.product.supplierID = selected.id;
  }

  selectedCategories(selected) {
    if(this.isAll) {
      this.selectCategory.options.first.deselect();
      this.isAll = !this.isAll; 
    }
    else if (this.selectCategory.options.length == selected.length + 1) {
      this.selectCategory.options.first.select();
      this.isAll = !this.isAll; 
    }
    else
      this.categories = selected;
  }

  selectedAllCategory() {
    this.isAll = !this.isAll; 
    if(this.isAll) {
      this.selectCategory.options.forEach((item : MatOption) => item.select());
    }
    else {
      this.selectCategory.options.forEach((item : MatOption) => item.deselect());
    }
    this.selectCategory.close();
  }

  compareCategory(c1: {id: number}, c2: {id: number}) {
    return c1 && c2 && c1.id === c2.id;
  }

  compareSupplier(c1: number , c2: number) {
    return c1 && c2 && c1 == c2;
  }
}
