import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, PageEvent } from '@angular/material';
import { ProductsStoreService } from '../../../services/store/products-store/products-store.service';
import { ProductService } from '../../../services/product/product.service';
import { faSortUp, faSortDown, faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { FormProductComponent } from '../form-product/form-product.component';
import { DeleteFormComponent } from '../delete-form/delete-form.component';
import { FilterParamsProduct } from 'src/app/services/model/filter-params-product.model';

@Component({
  selector: 'products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  @ViewChild('paginator', { static: false}) paginator: MatPaginator;

  filter: FilterParamsProduct = {
    pagenumber: 1,
    pagesize: 5,
    sort: null
  };
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
    this.productsStore.getAll(this.filter);
  }
  
  searchEvent($event) {
      this.filter = {
        pagenumber: 1,
        pagesize: this.filter.pagesize,
        sort: this.filter.sort,
        content: $event.content,
        idcategories: $event.idcategories,
        minprice: $event.minprice,
        maxprice: $event.maxprice,
        rating: $event.rating
      }
      this.paginator.pageIndex = 0;

      this.productsStore.getAll(this.filter)
  }

  reloadProduct() {
    this.filter = {
      pagenumber: 1,
      pagesize: this.filter.pagesize,
      sort: this.filter.sort
    }
    this.paginator.pageIndex = 0;
    this.productsStore.getAll(this.filter);
  }

  addProduct() {
    const dialogRef = this.dialog.open(FormProductComponent, {
      width: '500px',
      data: { typeform: ProductsListComponent.addForm, product: { }, categories: []
     }
    });

    dialogRef.afterClosed().subscribe(res => {
      if(res) {
        if(this.filter.sort == null && this.filter.pagenumber == 1) {
          this.productsStore.products.splice(this.filter.pagesize - 1,1);
          this.productsStore.products.splice(0,0,res);
          this.productsStore.totalPage = this.productsStore.totalPage + 1;
        }
        else {
          this.filter = {
            pagenumber: 1,
            pagesize: this.filter.pagesize,
            sort: null
          }
          this.productsStore.getAll(this.filter);
        }
        this.paginator.pageIndex = 0;
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
      let totalStore = this.productsStore.products.length;
      if(totalStore == 1) {
        this.filter.pagenumber = this.filter.pagenumber - 1;
        this.paginator.pageIndex = this.filter.pagenumber - 1;
      }
      this.productsStore.getAll(this.filter)
    });
    
  }
}
