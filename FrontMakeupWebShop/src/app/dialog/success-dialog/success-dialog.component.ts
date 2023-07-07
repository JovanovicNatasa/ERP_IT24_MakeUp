import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-success-dialog',
  template: `
    <h2 mat-dialog-title>Success</h2>
    <div mat-dialog-content>
      <p>{{ data }}</p>
    </div>
    <div mat-dialog-actions>
      <button mat-button [mat-dialog-close]="true">OK</button>
    </div>
  `,
})
export class SuccessDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: string) {}
}
