import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Subject } from 'rxjs';
import { Supplier } from '../model/supplier.model';
import { SupplierService } from '../services/supplier/supplier.service';

@Injectable({
  providedIn: 'root'
})
export class SuppliersStoreService {
  private readonly _suppliers = new BehaviorSubject<Supplier[]>([]);

  readonly suppliers$ = this._suppliers.asObservable();

  constructor(private supplierService: SupplierService, private toastr: ToastrService) {
    this.getAll(null, null, null);
   }

  get suppliers() : Supplier[] {
    return this._suppliers.value;
  }

  set suppliers(val:Supplier[]) {
    this._suppliers.next(val);
  }

  async getAll(pageNumber, pageSize, sort){
    await this.supplierService.getAll(pageNumber, pageSize, sort)
            .subscribe(res => this.suppliers = res,
              () => {
                this.toastr.error("An unexpected error occurred.")
              });
  }
}
