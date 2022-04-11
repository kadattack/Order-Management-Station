import { Component, OnInit } from '@angular/core';
import {AccountService} from "../_services/account.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  accountService: any;

  constructor(accountService: AccountService) {
    this.accountService = accountService;
  }

  ngOnInit(): void {
  }

}
