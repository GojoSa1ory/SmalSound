import {Component, inject, OnInit} from '@angular/core';
import {TrackService} from "../../service/track.service";
import {TrackModel} from "../../models/TrackModel";
import {UserModel} from "../../models/UserModel";
import {AuthService} from "../../service/auth.service";
import {TrackCardComponent} from "../../components/track-card/track-card.component";
import {AudioPlayerComponent} from "../../components/UI/audio-player/audio-player.component";


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    TrackCardComponent,
    AudioPlayerComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  private readonly tracService = inject(TrackService);
  private readonly authService = inject(AuthService);

  // request!: string
  // searchResult!: TrackModel[]
  // sortMethod: string = "byName"

  tracks!: TrackModel[]
  currentUser!: UserModel

  constructor() {
    this.currentUser = this.authService.getUser()
  }

  ngOnInit(): void {
    this.getTracks()
  }

  getTracks () {
    this.tracService.getAllTrack().subscribe({
      next: response => {
        this.tracks = response.data
      },
      error: err => console.log(err)
    })
  }

  // handleRequestChange () {
  //   this.tracService.searchTrack(this.request).subscribe({
  //     next: value => this.searchResult = value.data,
  //     error: err => console.log(err)
  //   })
  // }
  //
  // handleSortChange () {
  //   this.tracService.sortTrack(this.sortMethod).subscribe({
  //     next: value => this.searchResult = value.data,
  //     error: err => console.log(err)
  //   })
  // }

}
