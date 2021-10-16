import { ErrorHandler } from "@angular/core";
import { ToastrService } from "ngx-toastr";

export class AppErrorHandling implements ErrorHandler{
    toastr: ToastrService

    handleError(error: any): void {
        this.toastr.error("An unexpected error occurred.")
    }
}