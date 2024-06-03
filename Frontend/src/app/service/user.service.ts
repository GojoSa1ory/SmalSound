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

  updateUserInfo (user: any) {
    return this.http.patch(`${this.apiUrl}/user/update`, user, {headers: this.createAuthHeaders()}).subscribe({
      next: value => {
        this.notificationService.showNotification(false, "", "Данные обновлены")
        setTimeout(() => this.notificationService.closeNotification(), 2000 )
      },
      error: err => {
        this.notificationService.showNotification(true, "Ошибка", "")
        setTimeout(() => this.notificationService.closeNotification(), 2000 )
      }
    })
  }

  subscribeToUser (subscribedToId: number) {
    return this.http.post(`${this.apiUrl}/subscription/subscribe/${subscribedToId}`, null, {headers: this.createAuthHeaders()})
      .subscribe({
        next: value => {
          this.notificationService.showNotification(false, "", "Подписка оформлена")
          setTimeout(() => this.notificationService.closeNotification(), 2000 )
        },
        error: err => {
          this.notificationService.showNotification(true, "Ошибка")
          setTimeout(() => this.notificationService.closeNotification(), 2000 )
        }
    })
  }

  unsubscribeFromUser(subscribedToId: number) {
    return this.http.post(`${this.apiUrl}/subscription/unsubscribe/${subscribedToId}`,
      null,
      {headers: this.createAuthHeaders()}
    ).subscribe({
      next: value => {
        this.notificationService.showNotification(false, "", "Отписка оформлена")
        setTimeout(() => this.notificationService.closeNotification(), 2000 )
      },
      error: err => {
        this.notificationService.showNotification(true, "Ошибка")
        setTimeout(() => this.notificationService.closeNotification(), 2000 )
      }
    })
  }

  checkSub(subscribedToId: number) {
    return this.http.get<ServerResponseModel<boolean>>(`${this.apiUrl}/subscription/check/${subscribedToId}`, {headers: this.createAuthHeaders()})
  }

  getListenersCount(userId: number) {
    return this.http.get<ServerResponseModel<number>>(`${this.apiUrl}/subscription/listenersCount/${userId}`)
  }


  private createAuthHeaders (): HttpHeaders {
    return new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`)
  }
}
