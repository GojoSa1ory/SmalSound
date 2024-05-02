import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class PlayListService {

  playLists!: []
  private readonly apiUrl = environment.apiUrl;
  constructor(private readonly http: HttpClient) { }

  getUserPlayLists () {
    return this.http.get(`${this.apiUrl}/playLists/user/all`)
  }

  getUserPlayList () {
    return this.http.get(`${this.apiUrl}/playLists/user/all`)
  }
}
