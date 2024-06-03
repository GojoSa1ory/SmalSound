import { Injectable, WritableSignal, signal } from '@angular/core';
import {environment} from "../../environment/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {PlaylistModel} from "../models/playlist.model";
import {ServerResponseModel} from "../models/serverResponse.model";

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {

  private apiUrl = environment.apiUrl
  private playlists: WritableSignal<PlaylistModel[] | []> = signal([])
  private playlist: WritableSignal<PlaylistModel | null> = signal(null)
  trackId: WritableSignal<number> = signal(0)


  get playlists$ () {
    return this.playlists
  }
  get playlist$ () {
    return this.playlist
  }

  constructor(private http: HttpClient) { }

  createPlaylist () {
    const headers = this.createAuthHeaders()
    return this.http.post(`${this.apiUrl}/playlist/create`, {name: "Мой плейлист"}, {headers: headers})
  }

  addTrackToPlayList (playlistId: number, trackId: number) {
    const headers = this.createAuthHeaders()
    return this.http.post(`${this.apiUrl}/playlist/addTrack/${playlistId}/${trackId}`, null, {headers: headers})
  }

  getPlaylists () {
    return this.http.get<ServerResponseModel<PlaylistModel[]>>(`${this.apiUrl}/playlist/all`)
  }

  getPlaylist (id: number) {
    return this.http.get<ServerResponseModel<PlaylistModel>>(`${this.apiUrl}/playlist/one/${id}`)
  }

  getUserPlaylists (userId: number) {
    return this.http.get<ServerResponseModel<PlaylistModel[]>>(`${this.apiUrl}/playlist/all/user/${userId}`)
  }

  updatePlaylist (update: PlaylistModel, playlistId: number) {
    return this.http.patch(`${this.apiUrl}/playlist/update/${playlistId}`, update)
  }

  deletePlaylist (playlistId: number) {
    return this.http.delete(`${this.apiUrl}/playlist/remove/${playlistId}`)
  }

  deleteTrackFormPlaylist (playlistId: number, trackId: number) {
    const headers = this.createAuthHeaders()
    return this.http.delete(`${this.apiUrl}/playlist/removeTrack/${playlistId}/${trackId}`, {headers: headers})
  }

  autoAssignTracksToPlaylistsAsync () {
    return this.http.get<ServerResponseModel<PlaylistModel[]>>(`${this.apiUrl}/playlist/auto`)
  }

  private createAuthHeaders() {
    return new HttpHeaders().set(
      "Authorization",
      `Bearer ${localStorage.getItem("token")}`,
    );
  }

}
