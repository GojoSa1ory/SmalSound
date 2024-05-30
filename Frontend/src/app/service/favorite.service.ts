import {Injectable, signal, WritableSignal} from '@angular/core';
import {environment} from "../../environment/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ServerResponseModel} from "../models/serverResponse.model";
import {FavoriteModel} from "../models/favorite.model";

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {

  private apiUrl = environment.apiUrl

  favorite: WritableSignal<FavoriteModel | null> = signal(null)
  constructor(
    private http: HttpClient
  ) { }

  addTrackToFavorite (trackId: number) {
    const headers = this.createAuthHeaders()
    return this.http.post<ServerResponseModel<FavoriteModel>>(`${this.apiUrl}/favorite/user/add/${trackId}`, null, {headers: headers})
  }

  getFavorite () {
    const headers = this.createAuthHeaders()
    return this.http.get<ServerResponseModel<FavoriteModel>>(`${this.apiUrl}/favorite/user`, {headers: headers})
  }

  private createAuthHeaders() {
    return new HttpHeaders().set(
      "Authorization",
      `Bearer ${localStorage.getItem("token")}`,
    );
  }
}
