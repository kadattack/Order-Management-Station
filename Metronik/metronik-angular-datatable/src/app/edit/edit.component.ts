import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {take} from "rxjs/operators";
import {Oms} from "../interfaces/oms";
import {AccountService} from "../_services/account.service";
import {Order} from "../interfaces/order";
import {SerialNumbers} from "../interfaces/serial-numbers";
import {Router} from "@angular/router";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  panelOpenState = false;
  baseUrl= environment.apiUrl;
  currentOmsUser:Oms | undefined;
  constructor(private http : HttpClient, private accountService : AccountService, private router: Router) { }

  orderArray: Array<Order> | undefined;
  // orderProductInputs: OrderProducts | undefined

  ngOnInit(): void {
    this.getOrders()
  }


  getOrders(){
    this.accountService.currentUser$.pipe(take(1)).subscribe(savedOms => this.currentOmsUser = savedOms);

    if(this.currentOmsUser){
      this.http.get(this.baseUrl + "orders?omsId=" + this.currentOmsUser?.omsId).subscribe(
        (res:any) => {this.orderArray = res; console.log(res)}
      )
    }

  }

  editProduct(ordernum: number, productnum: number){
    let editedProd  =  this.orderArray![ordernum].emissionRequestDto.productsDto[productnum];
    editedProd.serialNumbersDto = editedProd.serialNumbersDto.filter(x=>x.serialNumber != '')
    this.http.put(this.baseUrl + 'orderProduct',  editedProd).subscribe(
      (res:any) => {console.log(res)}
    )
  }

  addSerial(orderNum:number, productNum:number){
    if(this.orderArray){
      let serialdot : SerialNumbers = {
      id: 0,
      appOrderProductId: this.orderArray[orderNum].emissionRequestDto.productsDto[productNum].id.toString(),
      serialNumber: ''
    }
      this.orderArray[orderNum].emissionRequestDto.productsDto[productNum].serialNumbersDto.push(serialdot)
    }
  }

  editOrderDetails(orderNum: number){
    let orderDetails = this.orderArray![orderNum].emissionRequestDto.orderDetailsDto
    this.http.put(this.baseUrl + 'orderDetails',  orderDetails).subscribe(
      (res:any) => {console.log(res)}
    )
  }


  deleteProduct(orderNum:number, productNum:number){
      let deleteProdId  =  this.orderArray![orderNum].emissionRequestDto.productsDto[productNum].id;
      this.http.delete(this.baseUrl + 'orderProduct/' + deleteProdId).subscribe(
        (res:any) => {console.log(res); this.getOrders()}
      )

  }

  deleteOrder(orderId : number){
    this.http.delete(this.baseUrl + 'orders/' + orderId.toString()).subscribe(
      (res:any) => {console.log(res); this.getOrders();
      }
    )
  }

}
