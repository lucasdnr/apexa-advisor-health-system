import { Injectable } from '@angular/core';
import { IndividualConfig, ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  constructor(private toastr: ToastrService) {}

  success(message?: string, title = 'Success'): void {
      this.toastr.success(title, message);
  }

  error(message?: string, title = 'Error', config: Partial<IndividualConfig> = {}): void {
      this.toastr.error(title, message, config);
  }

  info(message: string, title?: string, config: Partial<IndividualConfig> = {}): void {
    this.toastr.info(message, title, config);
  }

  closeAllToast(): void {
    this.toastr.clear();
  }
}
