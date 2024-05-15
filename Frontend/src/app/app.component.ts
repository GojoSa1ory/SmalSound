import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import {AudioPlayerComponent} from "./component/audio-player/audio-player.component";
import {TrackModel} from "./models/track.model";
import {TrackCardComponent} from "./component/track-card/track-card.component";
import {NavBarComponent} from "./component/nav-bar/nav-bar.component";
import {UserService} from "./service/user.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, AudioPlayerComponent, TrackCardComponent, NavBarComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  title = 'Frontend';
  tracks: WritableSignal<TrackModel[] | []> = signal([])

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe({
      next: value => {
        this.userService.user.set(value.data!)
        this.userService.isAuth.set(true)
      },
      error: err => console.log(err)
    })
  }

}
