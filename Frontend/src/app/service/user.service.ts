import {inject, Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {ServerResponse} from "../models/ServerResponse";
import {UserModel} from "../models/UserModel";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly apiUrl = environment.apiUrl
  private readonly http: HttpClient = inject(HttpClient)
  constructor() { }

  private getRequestOptions(): { headers: HttpHeaders } {
    let headers = new HttpHeaders();
    const token: string | null = sessionStorage.getItem("token")
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }
    return { headers };
  }
  getProfile (): Observable<ServerResponse<UserModel>> {
    const requestOptions = this.getRequestOptions();
    return this.http.get<ServerResponse<UserModel>>(`${this.apiUrl}/user/profile`, requestOptions)
  }

}
