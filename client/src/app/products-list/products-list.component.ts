import { Component, OnInit } from '@angular/core';
import { MatDialog, PageEvent } from '@angular/material';
import { FormProductComponent } from '../form-product/form-product/form-product.component';
import { SearchAdvance } from '../model/search-advance.model';
import { ProductsStoreService } from '../products-store/products-store.service';
import { ProductService } from '../services/product/product.service';
import { faSortUp, faSortDown, faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute } from '@angular/router';
import { DeleteFormComponent } from '../products/delete-form/delete-form/delete-form.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  lengthProduct: number;
  pageSize: number = 5;
  pageNumber: number;
  isSort: string = null;
  isSearch: boolean = false;
  searchObj: SearchAdvance;
  fasortup = faSortUp;
  fasortdown = faSortDown;
  faedit = faEdit;
  fatrash = faTrash;
  static readonly addForm = 0;
  static readonly editForm = 1;
  static readonly deleteForm = 2;

  constructor(private productsStore: ProductsStoreService, 
    public dialog: MatDialog,
    private productService: ProductService,
    private toastr: ToastrService) { 
    if(this.productsStore.products.length == 0) {
      this.productsStore.getAll(this.pageNumber, this.pageSize, this.isSort);
    }
  }

  ngOnInit() {
    
  }

  sortID() {
    if(this.productsStore.totalPages !== 0) {
      if(this.isSort != 'id:asc') {
        this.isSort = 'id:asc';
      }
      else {
        this.isSort = null;
      }
      this.productsStore.getAll(this.pageNumber, this.pageSize, this.isSort);
    }
  }

  sortName() {
    if(this.productsStore.totalPages !== 0) {
      if(this.isSort != 'name:asc') {
        this.isSort = 'name:asc';
      }
      else {
        this.isSort = 'name:desc';
      }
      
      this.productsStore.getAll(this.pageNumber, this.pageSize, this.isSort);
    }
  }

  sortRating() {
    if(this.productsStore.totalPages !== 0) {
      if(this.isSort != 'rating:asc') {
        this.isSort = 'rating:asc';
      }
      else {
        this.isSort = 'rating:desc'
      }

      this.productsStore.getAll(this.pageNumber, this.pageSize, this.isSort);
    }
  }
  
  sortPrice() {
    if(this.productsStore.totalPages !== 0) {
      if(this.isSort != 'price:asc') {
        this.isSort = 'price:asc'
      }
      else {
        this.isSort = 'price:desc'
      }
      
      this.productsStore.getAll(this.pageNumber, this.pageSize, this.isSort);
    }
  }

  onPaginate(pageEvent: PageEvent) {
    this.pageSize = +pageEvent.pageSize;
    this.pageNumber = +pageEvent.pageIndex + 1;
    if(this.isSearch) {
      this.productsStore.searchProduct(this.pageNumber, this.pageSize, this.isSort, this.searchObj.idCategories, this.searchObj.search);
    }
    else {
      this.productsStore.getAll(this.pageNumber, this.pageSize, this.isSort);
    }
  }
  
  searchEvent($event) {
    if($event.search == '') {
      this.isSearch = false;
      this.productsStore.getAll(1, this.pageSize, this.isSort);
    }
    else {
      this.isSearch = true;
      this.searchObj = $event;
      this.productsStore.searchProduct(1, this.pageSize, this.isSort, this.searchObj.idCategories, this.searchObj.search);
    }
  }

  addProduct() {
    const dialogRef = this.dialog.open(FormProductComponent, {
      width: '500px',
      data: { typeform: ProductsListComponent.addForm, product: { }, categories: []
     }
    });

    dialogRef.afterClosed().subscribe(res => {
      if(res) {
        if(this.isSort === null) {
          this.productsStore.products.splice(this.pageSize - 1,1);
          this.productsStore.products.splice(0,0,res);
        }
        else {
          this.productsStore.getAll(1, this.pageSize, null);
        }
      }
    });
  }

  editProduct(idProduct) {
    if(!this.productsStore.products.find(p => p.id == idProduct)) {
      this.toastr.error("Cannot find the product #" + idProduct)
    }
    else {
      this.productsStore.getProduct(idProduct).subscribe(res => {
        if(res) {
          const dialogRef = this.dialog.open(FormProductComponent, {
            width: '500px',
            data: { typeform: ProductsListComponent.editForm, product: res.product, categories: res.categories }
          });
          
          dialogRef.afterClosed().subscribe(res => {
            if(res) {
              var index = this.productsStore.products.findIndex(p => p.id == res.id)
              this.productsStore.products.splice(index, 1, res)
            }
          });
        }
      })
    }
  }

  deleteProduct(idProduct) {
    const dialogRef = this.dialog.open(DeleteFormComponent, {
      data: idProduct
    });

    dialogRef.afterClosed().subscribe(() => {
      this.productsStore.getAll(this.pageNumber, this.pageSize, this.isSort)
    });
  }
}
