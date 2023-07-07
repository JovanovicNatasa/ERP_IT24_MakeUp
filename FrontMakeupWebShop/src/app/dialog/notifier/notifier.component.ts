import { Component, OnInit } from '@angular/core';
import{ToastrService} from 'ngx-toastr'

@Component({
  selector: 'app-notifier',
  templateUrl: './notifier.component.html',
  styleUrls: ['./notifier.component.css']
})
export class NotifierComponent implements OnInit {

  constructor(private toastrService:ToastrService) { }

  ngOnInit(): void {
  }
  /*ShowSucces(message:string){
    this.toastrService.success(message)
  }

  ShowError(message:string){
    this.toastrService
  }*/

}
