import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ServerResponse} from "../models/ServerResponse";
import {TrackModel} from "../models/TrackModel";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class TrackService {

  private readonly apiUrl = environment.apiUrl
  constructor(private http: HttpClient) { }

  getAllTrack ():Observable<ServerResponse<TrackModel[]>> {
    return this.http.get<ServerResponse<TrackModel[]>>(`${this.apiUrl}/track/all`)
  }

}
