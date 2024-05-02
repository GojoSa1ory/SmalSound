import {Inject, Injectable} from '@angular/core';
import {AuthModel, SetUserModel, UserModel} from "../models/UserModel";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ServerResponse} from "../models/ServerResponse";
import {DOCUMENT} from "@angular/common";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly apiUrl = environment.apiUrl
  private user!: UserModel
  isAuth: boolean = false;
  token: string | null = null

  constructor(
    private http: HttpClient,
    @Inject(DOCUMENT) private document: Document
  ) {
    const localStorage = document.defaultView?.localStorage
    if(localStorage){
      this.token = localStorage.getItem("token")
    }

  }

  getUser (): UserModel {
    return this.user
  }

  setUser(user: UserModel): void {
    this.user = user;
  }

  loginUser(user: SetUserModel): Observable<ServerResponse<AuthModel>> {
    return this.http.post<ServerResponse<AuthModel>>(`${this.apiUrl}/auth/login`, user)
  }

  registerUser(user: SetUserModel): Observable<ServerResponse<AuthModel>> {
    return this.http.post<ServerResponse<AuthModel>>(`${this.apiUrl}/auth/register`, user)
  }
}
