import {Injectable, signal, WritableSignal} from '@angular/core';
import {UserModel} from "../models/user.model";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environment/environment";
import {Observable, Subscription} from "rxjs";
import {ServerResponseModel} from "../models/serverResponse.model";
import {Router} from "@angular/router";
import {NotificationService} from "./notification.service";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl: string = environment.apiUrl
  private user: WritableSignal<UserModel | null> = signal(null)

  get user$ () {
    return this.user
  }
  constructor(
    private http: HttpClient,
    private router: Router,
    private notificationService: NotificationService
  ) { }

  getUserProfile (userId: number):Subscription {
    return this.http.get<ServerResponseModel<UserModel>>(`${this.apiUrl}/user/user/${userId}`).subscribe({
      next: value => {
        this.user.set(value.data!)
      },
      error: err => {
        this.router.navigate(['404'])
        console.log(err)
      }
    })
  }

  private createAuthHeaders (): HttpHeaders {
    return new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`)
  }
}
