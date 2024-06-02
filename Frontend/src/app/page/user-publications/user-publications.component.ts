import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import {TrackModel} from "../../models/track.model";
import {TrackService} from "../../service/track.service";
import {TrackCardComponent} from "../../component/UI/track-card/track-card.component";
import {AudioPlayerService} from "../../service/audio-player.service";
import {AuthService} from "../../service/auth.service";

@Component({
  selector: 'app-user-publications',
  standalone: true,
  imports: [
    TrackCardComponent
  ],
  templateUrl: './user-publications.component.html',
  styleUrl: './user-publications.component.scss'
})
export class UserPublicationsComponent implements OnInit{

  tracks: WritableSignal<TrackModel[] | []> = signal([]);

  constructor(
    private trackService: TrackService,
    private audioPlayerService: AudioPlayerService,
    private authService:AuthService
  ) {
    this.tracks = this.trackService.userTracks
  }
  ngOnInit(): void {
    this.trackService.getAllUserTracks(this.authService.user$()!.id).subscribe({
      next: value => {
        this.trackService.userTracks.set(value.data!)
        this.audioPlayerService.tracks.set(value.data!)
      },
      error: err => console.log(err)
    })
  }



}
