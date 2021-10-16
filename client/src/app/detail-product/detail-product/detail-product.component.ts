import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/model/category.model';
import { FormProduct } from 'src/app/model/form-product.model';
import { ProductsStoreService } from 'src/app/products-store/products-store.service';
import { SuppliersStoreService } from 'src/app/suppliers-store/suppliers-store.service';

@Component({
  selector: 'detail-product',
  templateUrl: './detail-product.component.html',
  styleUrls: ['./detail-product.component.css']
})
export class DetailProductComponent implements OnInit {
  response: FormProduct
  id: number
  supplierName: string
  categories: Category[]

  constructor(private route: ActivatedRoute,
    private router: Router,
    private productStore: ProductsStoreService,
    private supplierStore: SuppliersStoreService) { 

    this.route.params.subscribe((param) => {
      this.id = param['id']
      if(!this.productStore.products.find(p => p.id == this.id)) {
        this.router.navigate(['/not-found'])
      }
      this.productStore.getProduct(param['id']).subscribe(res => {
        this.response = res;
        this.supplierName = this.supplierStore.suppliers.filter(s => s.id == this.response.product.supplierID).pop().name
        this.categories = this.response.categories;
        
      })
    });
  }

  ngOnInit() {

  }

}
