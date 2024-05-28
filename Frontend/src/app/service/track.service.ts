import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, WritableSignal, signal } from "@angular/core";
import { environment } from "../../environment/environment";
import { ServerResponseModel } from "../models/serverResponse.model";
import { GenreModel, SetTrackModel, TrackModel } from "../models/track.model";
import { Observable, Subscription } from "rxjs";
import { CloudOffIcon } from "lucide-angular";

@Injectable({
    providedIn: "root",
})
export class TrackService {
    private apiUrl = environment.apiUrl;

    tracks: WritableSignal<TrackModel[] | []> = signal([]);
    userTracks: WritableSignal<TrackModel[] | []> = signal([]);
    genres: WritableSignal<GenreModel[] | []> = signal([]);

    constructor(private http: HttpClient) {}

    getAllTracks(): Observable<ServerResponseModel<TrackModel[]>> {
        return this.http.get<ServerResponseModel<TrackModel[]>>(
            `${this.apiUrl}/track/all`,
        );
    }

    getAllUserTracks(): Observable<ServerResponseModel<TrackModel[]>> {
        const headers = this.createAuthHeaders();

        return this.http.get<ServerResponseModel<TrackModel[]>>(
            `${this.apiUrl}/track/user/all`,
            { headers: headers },
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
                error: (err) => console.log(err),
            });
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
