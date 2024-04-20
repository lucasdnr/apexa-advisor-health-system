import { NgIf } from '@angular/common';
import { Component, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-dialog',
  standalone: true,
  imports: [NgIf, MatButtonModule],
  templateUrl: './dialog.component.html',
  styleUrl: './dialog.component.scss'
})
export class DialogComponent{
  title: string = 'Notification!';
  message: string = '';
  yes: string = "Ok";
  no: any = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<DialogComponent>,
  ) {
    this.title = this.data.title;
    this.message = this.data.message;
    if (this.data.yes)
      this.yes = this.data.yes;

    if (this.data.no)
      this.no = this.data.no;
  }

  close(response: boolean) {
    this.dialogRef.close(response);
  }

}
