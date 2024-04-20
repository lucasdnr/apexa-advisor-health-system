import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DialogComponent } from './dialog.component';
import { ComponentType } from '@angular/cdk/portal';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private dialog: MatDialog) { }

  showDialogError(title: string, message: string) {
    return this.dialog.open(DialogComponent, {
      data: {
        title: title,
        message: message,
        yes: false,
        no: false
      }
    });
  }

  showConfirmationdialog(title: string, message: string, yes?: string, no?: string) {
    let dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: title,
        message: message,
        yes: yes,
        no: no
      }
    });

    return dialogRef.afterClosed();
  }

  customDialog(component: ComponentType<unknown>, data: MatDialogConfig) {
    let dialogRef = this.dialog.open(component, data);

    return dialogRef.afterClosed();
  }
}
