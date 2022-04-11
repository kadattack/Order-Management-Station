import { Component, OnInit } from '@angular/core';
import {AccountService} from "../_services/account.service";
import {FormBuilder} from "@angular/forms";
import { Router} from "@angular/router";
import {Oms} from "../interfaces/oms";




@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private formBuilder;
  omsForm: any
  private router: Router;
  private omsObject? : Oms = undefined;

  constructor(public accountService: AccountService, formBuilder : FormBuilder, router: Router) {
    accountService;
    this.formBuilder = formBuilder;
    this.router = router;
  }


  ngOnInit(): void {
    this.omsForm = this.formBuilder.group({
      omsId:['0645848']
    })
  }



  login(){
    this.omsObject = { id:"", clientToken: "", omsId: this.omsForm.value.omsId}
    this.accountService.login(this.omsObject).subscribe(res => {
      this.router.navigateByUrl("/create");
    }, error => {
      console.log(error)
    })
  }



}
