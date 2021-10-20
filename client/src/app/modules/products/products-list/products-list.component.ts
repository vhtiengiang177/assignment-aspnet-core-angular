import { Component, OnInit } from '@angular/core';
import { MatDialog, PageEvent } from '@angular/material';
import { SearchAdvance } from '../../../services/model/search-advance.model';
import { ProductsStoreService } from '../../../services/store/products-store/products-store.service';
import { ProductService } from '../../../services/product/product.service';
import { faSortUp, faSortDown, faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { FormProductComponent } from '../form-product/form-product.component';
import { DeleteFormComponent } from '../delete-form/delete-form.component';
import { FilterParamsProduct } from 'src/app/services/model/filter-params-product.model';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  lengthProduct: number;
  filter: FilterParamsProduct = {
    pagesize: 5,
    sort: null
  };
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
    
  }

  ngOnInit() {
    
  }

  sortID() {
    if(this.productsStore.totalPage !== 0) {
      if(this.filter.sort != 'id:asc') {
        this.filter.sort = 'id:asc';
      }
      else {
        this.filter.sort = null;
      }
      this.productsStore.getAll(this.filter);
    }
  }

  sortName() {
    if(this.productsStore.totalPage !== 0) {
      if(this.filter.sort != 'name:asc') {
        this.filter.sort = 'name:asc';
      }
      else {
        this.filter.sort = 'name:desc';
      }
      
      this.productsStore.getAll(this.filter);
    }
  }

  sortRating() {
    if(this.productsStore.totalPage !== 0) {
      if(this.filter.sort != 'rating:asc') {
        this.filter.sort = 'rating:asc';
      }
      else {
        this.filter.sort = 'rating:desc'
      }

      this.productsStore.getAll(this.filter);
    }
  }
  
  sortPrice() {
    if(this.productsStore.totalPage !== 0) {
      if(this.filter.sort != 'price:asc') {
        this.filter.sort = 'price:asc'
      }
      else {
        this.filter.sort = 'price:desc'
      }
      
      this.productsStore.getAll(this.filter);
    }
  }

  onPaginate(pageEvent: PageEvent) {
    this.filter.pagesize = +pageEvent.pageSize;
    this.filter.pagenumber = +pageEvent.pageIndex + 1;
    if(this.isSearch) {
      this.productsStore.filterProduct(filter)
    }
    else {
      this.productsStore.getAll(this.filter);
    }
  }
  
  searchEvent($event) {
    this.filter.pagenumber = 1;
    if(!$event.search) {
      this.isSearch = false;
      this.productsStore.getAll(this.filter);
    }
    else {
      this.isSearch = true;
      this.searchObj = $event;
      this.filter.content = $event.content;
      this.filter.idcategories = $event.idcategories;
      this.filter.minprice = $event.minprice;
      this.filter.maxprice = $event.maxprice;
      this.filter.rating = $event.rating;
      
      this.productsStore.filterProduct(this.filter)
      //this.productsStore.searchProduct(1, this.pageSize, this.isSort, this.searchObj.idCategories, this.searchObj.search);
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
          this.filter.pagenumber = 1,
          this.filter.sort = null
          this.productsStore.getAll(this.filter);
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
      this.productsStore.getAll(this.filter)
    });
  }
}
