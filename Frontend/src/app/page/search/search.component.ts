import {Component, OnDestroy, OnInit, signal, WritableSignal} from '@angular/core';
import {SearchModel} from "../../models/search.model";
import {SearchService} from "../../service/search.service";
import {UserCardComponent} from "../../component/UI/user-card/user-card.component";
import {TrackCardComponent} from "../../component/UI/track-card/track-card.component";
import {TrackService} from "../../service/track.service";
import {GenreModel} from "../../models/track.model";
import {PlaylistCardComponent} from "../../component/UI/playlist-card/playlist-card.component";

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [
    UserCardComponent,
    TrackCardComponent,
    PlaylistCardComponent
  ],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent implements OnDestroy, OnInit {

  request: string = ""
  result: WritableSignal<SearchModel | null> = signal(null)
  genres: WritableSignal<GenreModel[] | []> = signal([])
  error: WritableSignal<boolean> = signal(false)


  constructor(
    private searchService: SearchService,
    private trackService: TrackService
  ) {
    this.result = this.searchService.result$
    this.error = this.searchService.error$
    this.genres = this.trackService.genres
  }

  ngOnInit(): void {
    this.trackService.getGenres()
  }

  ngOnDestroy(): void {
    this.result.set(null)
    this.request = ""
  }

  getSearch(event: Event) {
    this.request = (event.target as HTMLInputElement).value
    this.searchService.search(this.request)
  }

  filterTrack(genreId: number) {
    this.searchService.filterTracks(genreId)
  }

  sortTrack (event: any) {
    if(event.target.value != "0"){
      this.searchService.sortTracks(event.target.value)
    }
  }
}
