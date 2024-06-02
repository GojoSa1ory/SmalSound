import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, WritableSignal, signal } from "@angular/core";
import { environment } from "../../environment/environment";
import { ServerResponseModel } from "../models/serverResponse.model";
import { GenreModel, SetTrackModel, TrackModel } from "../models/track.model";
import { Observable, Subscription } from "rxjs";
import { CloudOffIcon } from "lucide-angular";
import {NotificationService} from "./notification.service";

@Injectable({
    providedIn: "root",
})
export class TrackService {
    private apiUrl = environment.apiUrl;

    tracks: WritableSignal<TrackModel[] | []> = signal([]);
    userTracks: WritableSignal<TrackModel[] | []> = signal([]);
    genres: WritableSignal<GenreModel[] | []> = signal([]);

    constructor(
      private http: HttpClient,
      private notificationService: NotificationService
    ) {}

    getAllTracks(): Observable<ServerResponseModel<TrackModel[]>> {
        return this.http.get<ServerResponseModel<TrackModel[]>>(
            `${this.apiUrl}/track/all`,
        );
    }

    getAllUserTracks(id: number): Observable<ServerResponseModel<TrackModel[]>> {
        return this.http.get<ServerResponseModel<TrackModel[]>>(
            `${this.apiUrl}/track/user/all/${id}`
        );
    }

    uploadTrack(track: any) {
        const headers = new HttpHeaders().set(
            "Authorization",
            `Bearer ${localStorage.getItem("token")}`,
        );

        return this.http
            .post(`${this.apiUrl}/track/create`, track, {
                headers: headers,
                responseType: "json",
            })
            .subscribe({
                next: () => {
                  this.notificationService.showNotification(false, "", "Трек загружен успешно")
                  setTimeout(() => {this.notificationService.closeNotification()}, 2000)
                },
                error: (err) => {
                  this.notificationService.showNotification(true, "Ошибка при отправке трека")
                  setTimeout(() => {this.notificationService.closeNotification()}, 2000)
                },
            });
    }

    deleteTrack (trackId: number) {
      const headers = new HttpHeaders().set(
        "Authorization",
        `Bearer ${localStorage.getItem("token")}`,
      );

      return this.http.delete(`${this.apiUrl}/track/delete/${trackId}`, {headers: headers})
    }

    getGenres(): Subscription {
        return this.http
            .get<
                ServerResponseModel<GenreModel[]>
            >(`${this.apiUrl}/track/genres`)
            .subscribe({
                next: (value) => {
                    this.genres.set(value.data!);
                },
                error: (err) => {
                    console.log(err);
                },
            });
    }

    private createAuthHeaders() {
        return new HttpHeaders().set(
            "Authorization",
            `Bearer ${localStorage.getItem("token")}`,
        );
    }
}
