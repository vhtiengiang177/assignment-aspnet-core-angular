import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { throwError } from 'rxjs';
import { ProductsStoreService } from 'src/app/services/store/products-store/products-store.service';
import { AppError } from 'src/app/_shared/errors/app-error';
import { BadRequestError } from 'src/app/_shared/errors/bad-request';
import { NotFoundError } from 'src/app/_shared/errors/not-found-error';

@Component({
  selector: 'app-delete-form',
  templateUrl: './delete-form.component.html',
  styleUrls: ['./delete-form.component.css']
})
export class DeleteFormComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<DeleteFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private productStore: ProductsStoreService,
    private toastr: ToastrService) { }

  ngOnInit() {
    
  }

  delete() {
    this.productStore.delete(this.data).subscribe(() => {
      this.dialogRef.close();
    }, (error: AppError) => {
      if(error instanceof NotFoundError)
        this.toastr.error("The product #" + this.data + " not found")
      else if (error instanceof BadRequestError)
        this.toastr.error("Failed to delete the product #" + this.data)
      else throwError(error)
      this.dialogRef.close()
    })
  }

}
