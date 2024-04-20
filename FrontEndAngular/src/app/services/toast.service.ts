import { Injectable } from '@angular/core';
import { IndividualConfig, ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  constructor(private toastr: ToastrService) {}

  success(message?: string, title = 'Success'): void {
      this.toastr.success(message, title);
  }

  error(message?: string, title = 'Error', config: Partial<IndividualConfig> = {}): void {
      this.toastr.error(message, title, config);
  }

  errorGeneric(config: Partial<IndividualConfig> = {}): void {
    const message = 'An error occurred. Please try again later';
    this.toastr.error(message,  'Error', config);
  }

  info(message: string, title?: string, config: Partial<IndividualConfig> = {}): void {
    this.toastr.info(message, title, config);
  }

  closeAllToast(): void {
    this.toastr.clear();
  }
}
