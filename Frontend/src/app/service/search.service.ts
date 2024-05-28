import {Injectable, WritableSignal, signal} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environment/environment";
import {Subscription} from "rxjs";
import {ServerResponseModel} from "../models/serverResponse.model";
import {SearchModel} from "../models/search.model";
import {TrackModel} from "../models/track.model";

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  private API_URL = environment.apiUrl
  private result: WritableSignal<SearchModel | null> = signal(null)
  private error: WritableSignal<boolean> = signal(false)

  get result$ () {
    return this.result
  }

  get error$ () {
    return this.error
  }

  constructor(
    private http: HttpClient
  ) { }

  search (request: string): Subscription {
    return this.http.get<ServerResponseModel<SearchModel>>(`${this.API_URL}/search/search/${request}`).subscribe({
      next: value => {
        this.result.set(value.data!)
        this.error.set(false)
      },
      error: err => {
        console.log(err)
        this.result.set(null)
        this.error.set(true)
      }
    })
  }

  filterTracks (filterId: number): Subscription {
    return this.http.get<ServerResponseModel<SearchModel>>(`${this.API_URL}/search/filter/${filterId}`).subscribe({
      next: value => {
        this.result.set(value.data!)
        this.error.set(false)
      },
      error: err => {
        console.log(err)
        this.result.set(null)
        this.error.set(true)
      }
    })
  }

  sortTracks (method: string): Subscription {
    return this.http.get<ServerResponseModel<SearchModel>>(`${this.API_URL}/search/sort/${method}`).subscribe({
      next: value => {
        this.result.set(value.data!)
        this.error.set(false)
      },
      error: err => {
        console.log(err)
        this.result.set(null)
        this.error.set(true)
      }
    })
  }
}
