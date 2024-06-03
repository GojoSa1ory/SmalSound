import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import {TrackService} from "../../service/track.service";
import {TrackModel} from "../../models/track.model";
import { TrackCardComponent } from '../../component/UI/track-card/track-card.component';
import {PlaylistService} from "../../service/playlist.service";
import {PlaylistModel} from "../../models/playlist.model";
import {PlaylistCardComponent} from "../../component/UI/playlist-card/playlist-card.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    TrackCardComponent,
    PlaylistCardComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit{

    tracks: WritableSignal<TrackModel[] | []> = signal([])
    playlists: PlaylistModel[] | [] = []

    constructor(
      private trackService: TrackService,
      private playlistSerivice: PlaylistService
    ) {
      this.tracks = this.trackService.tracks
    }
    ngOnInit(): void {
        this.playlistSerivice.autoAssignTracksToPlaylistsAsync().subscribe({
          next: value => {
            this.playlists = value.data!
          },
          error: err => {}
        })

        this.trackService.getAllTracks().subscribe({
          next: value => {
            this.trackService.tracks.set(value.data!)
          },
          error: err => console.log(err)
        })
    }

}
