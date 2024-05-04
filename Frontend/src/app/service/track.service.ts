import { HttpClient } from "@angular/common/http";
import { Injectable, WritableSignal, signal } from "@angular/core";
import { environment } from "../../environment/environment";
import { ServerResponseModel } from "../models/serverResponse.model";
import { TrackModel } from "../models/track.model";
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root",
})
export class TrackService {
    private apiUrl = environment.apiUrl;

    tracks: WritableSignal<TrackModel[] | []> = signal([]);

    constructor(private http: HttpClient) {}

    getAllTracks(): Observable<ServerResponseModel<TrackModel[]>> {
        return this.http.get<ServerResponseModel<TrackModel[]>>(
            `${this.apiUrl}/track/all`,
        );
    }
}
