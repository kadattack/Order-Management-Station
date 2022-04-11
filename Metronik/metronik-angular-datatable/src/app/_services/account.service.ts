import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {ReplaySubject} from "rxjs";
import {Oms} from "../interfaces/oms";
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl= environment.apiUrl;

  private currentUserSource = new ReplaySubject<Oms>(1)
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient) { }



  login(model:Oms){
    return this.http.post(this.baseUrl + "login", model).pipe(
      map((response:any) =>{
        const oms = response;
        if (oms){
          this.setCurrentOmsUser(oms)
        }
      }))
  }

  setCurrentOmsUser(omsUser:Oms){
    localStorage.setItem('omsUser',JSON.stringify(omsUser));
    this.currentUserSource.next(omsUser)

  }

  logout(){
    localStorage.removeItem('omsUser');
    this.currentUserSource.next(undefined)
  }

}
