import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { throwError } from 'rxjs';
import { AppError } from 'src/app/common/app-error';
import { BadRequestError } from 'src/app/common/bad-request';
import { NotFoundError } from 'src/app/common/not-found-error';
import { ProductsStoreService } from 'src/app/products-store/products-store.service';

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
