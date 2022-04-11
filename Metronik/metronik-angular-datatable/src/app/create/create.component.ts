import {Component, OnInit, ViewChild} from '@angular/core';
import {Order} from "../interfaces/order";
import {HttpClient} from "@angular/common/http";
import {AccountService} from "../_services/account.service";
import {EmissionRequestDto} from "../interfaces/emission-request-dto";
import {take} from "rxjs/operators";
import {SerialNumbers} from "../interfaces/serial-numbers";
import {FormGroupDirective} from "@angular/forms";
import {environment} from "../../environments/environment";
import {MatSnackBar} from '@angular/material/snack-bar';
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  request:EmissionRequestDto = {
    omsId : '',
    orderDetailsDto : {
      id: '',
      expectedStartDate: '',
      omsId:'',
      poNumber:'',
      productCode:'',
      productDescription:'',
      productionLineId:'',
      factoryCountry:'',
      factoryAddress:'',
      factoryName:'',
      factoryId:''
    } ,
    productsDto: [{
      id:0,
      gtin:'',
      quantity: 0,
      serialNumbersDto:[],
      serialNumberType:'',
      templateId:''
    }]
  };

  @ViewChild('orderForm') orderForm: FormGroupDirective | undefined;
  @ViewChild('orderDetailsForm') orderDetailsForm: FormGroupDirective | undefined;
  baseUrl= environment.apiUrl;

  constructor(private http : HttpClient, private accountService : AccountService, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe(savedOms => this.request.omsId = savedOms.omsId);
  }




  createOrder(){

    this.http.post(this.baseUrl + "orders" , this.request).subscribe(
      (res:any) => {console.log(res);
        this._snackBar.open("Order created with\nOmsId: " + res.omsId + "\nOrderId: "
          + res.orderId + "\nEexpectedCompletionTime: " + res.expectedCompletionTime ,
          "Ok", {panelClass: ['success-snackbar']});
      }
    )

  }

  addSerial(productNum: number){
      let serialdot : SerialNumbers = {
        id: 0,
        appOrderProductId: this.request.productsDto[productNum].id.toString(),
        serialNumber: ''
      }
    this.request.productsDto[productNum].serialNumbersDto.push(serialdot)
    }

  addOrderProductForm(){
    let newProductForm = {
      id:0,
      gtin:'',
      quantity: 0,
      serialNumbersDto:[],
      serialNumberType:'',
      templateId:''
    }
    this.request.productsDto.push(newProductForm);
  }

}
