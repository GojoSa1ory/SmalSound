import {Injectable, signal, WritableSignal} from '@angular/core';
import {UserModel} from "../models/user.model";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environment/environment";
import {Observable} from "rxjs";
import {ServerResponseModel} from "../models/serverResponse.model";
import {AuthRequestModel, AuthResponseModel} from "../models/auth.model";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl: string = environment.apiUrl
  user: WritableSignal<UserModel | null> = signal(null)
  isAuth: WritableSignal<boolean> = signal(false)

  constructor(private http: HttpClient) { }

  loginUser (user: AuthRequestModel):Observable<ServerResponseModel<AuthResponseModel>> {
    return this.http.post<ServerResponseModel<AuthResponseModel>>(`${this.apiUrl}/auth/login`, user)
  }

  registerUser (user: AuthRequestModel):Observable<ServerResponseModel<AuthResponseModel>> {
    return this.http.post<ServerResponseModel<AuthResponseModel>>(`${this.apiUrl}/auth/register`, user)
  }

  getUserProfile ():Observable<ServerResponseModel<UserModel>> {

    const headers = this.createAuthHeaders()

    return this.http.get<ServerResponseModel<UserModel>>(`${this.apiUrl}/user/profile`, {headers: headers} )
  }

  private createAuthHeaders () {
    return new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem("token")}`)
  }
}
