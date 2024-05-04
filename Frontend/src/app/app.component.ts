import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import {TrackService} from "./service/track.service";
import {AudioPlayerComponent} from "./component/audio-player/audio-player.component";
import {TrackModel} from "./models/track.model";
import {TrackCardComponent} from "./component/track-card/track-card.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, AudioPlayerComponent, TrackCardComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'Frontend';
  tracks: WritableSignal<TrackModel[] | []> = signal([])

  constructor(private trackService: TrackService) {}

  ngOnInit(): void {
      this.trackService.getAllTracks().subscribe({
        next: value => this.trackService.tracks.set(value.data!),
        error: err => console.log(err)
      })
      this.tracks = this.trackService.tracks
  }
}
