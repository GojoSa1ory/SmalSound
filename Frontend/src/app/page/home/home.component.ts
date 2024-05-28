import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import {TrackService} from "../../service/track.service";
import {TrackModel} from "../../models/track.model";
import { TrackCardComponent } from '../../component/UI/track-card/track-card.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    TrackCardComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit{

    tracks: WritableSignal<TrackModel[] | []> = signal([])

    constructor(
      private trackService: TrackService,
    ) {
      this.tracks = this.trackService.tracks
    }
    ngOnInit(): void {
        this.trackService.getAllTracks().subscribe({
          next: value => this.trackService.tracks.set(value.data!),
          error: err => console.log(err)
        })
    }

}
