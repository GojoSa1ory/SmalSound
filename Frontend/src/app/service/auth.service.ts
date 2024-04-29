import { Injectable } from '@angular/core';
import {AuthModel, SetUserModel, UserModel} from "../models/UserModel";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ServerResponse} from "../models/ServerResponse";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly apiUrl = environment.apiUrl
  user!: UserModel
  constructor(private http: HttpClient) { }

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
