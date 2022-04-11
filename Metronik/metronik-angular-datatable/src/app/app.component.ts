import {Component, OnInit} from '@angular/core';
import {AccountService} from "./_services/account.service";
import {Oms} from "./interfaces/oms";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'metronik-angular-datatable';


  constructor(private accountService: AccountService) {
  }
  setCurrentUser(){
    const id = localStorage.getItem('omsUser')
    if (id){
      const omsId:Oms = JSON.parse(id)
      this.accountService.setCurrentOmsUser(omsId);
    }
  }


  ngOnInit(): void {
    this.setCurrentUser()
  }
}
