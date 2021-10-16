import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { async } from '@angular/core/testing';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MatOption, MatSelect, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { CategoriesStoreService } from 'src/app/categories-store/categories-store.service';
import { AppError } from 'src/app/common/app-error';
import { BadRequestError } from 'src/app/common/bad-request';
import { Category } from 'src/app/model/category.model';
import { FormProduct } from 'src/app/model/form-product.model';
import { Product } from 'src/app/model/product.model';
import { Supplier } from 'src/app/model/supplier.model';
import { ProductsListComponent } from 'src/app/products-list/products-list.component';
import { ProductsStoreService } from 'src/app/products-store/products-store.service';
import { DataService } from 'src/app/services/data/data.service';
import { ProductService } from 'src/app/services/product/product.service';
import { SuppliersStoreService } from 'src/app/suppliers-store/suppliers-store.service';

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
  newId: number;
  title: string;
  
  constructor(public dialogRef: MatDialogRef<FormProductComponent>,
    @Inject(MAT_DIALOG_DATA) public data: FormProduct,
    private categoriesStore: CategoriesStoreService,
    private suppliersStore: SuppliersStoreService,
    private productsStore: ProductsStoreService,
    private toastr: ToastrService) {
    
      
    }

  ngOnInit() {
    if(this.data.categories.length == this.categoriesStore.categories.length) {
      this.data.categories.unshift({id: 0, name: 'All'});
      this.isAll = !this.isAll;
    }
  }

  save() {
    if(!this.data.product.name || !this.data.product.rating || !this.data.product.price || !this.selectSupplier.value) {
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
          this.toastr.success("The product has been edited")
        }, (error : AppError) => {
          if(error instanceof BadRequestError)
            this.toastr.error("Edit product failed")
          else throw error
        })
      }
      else this.toastr.warning("Invalid");
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
