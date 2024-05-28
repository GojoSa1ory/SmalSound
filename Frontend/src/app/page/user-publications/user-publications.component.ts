import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import {TrackModel} from "../../models/track.model";
import {TrackService} from "../../service/track.service";
import {TrackCardComponent} from "../../component/UI/track-card/track-card.component";

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

  constructor(private trackService: TrackService) {
    this.tracks = this.trackService.userTracks
  }
  ngOnInit(): void {
    this.trackService.getAllUserTracks().subscribe({
      next: value => {
        this.trackService.userTracks.set(value.data!)
      },
      error: err => console.log(err)
    })
  }



}
