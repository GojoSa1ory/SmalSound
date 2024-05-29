import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import {AudioPlayerComponent} from "./component/audio-player/audio-player.component";
import {TrackModel} from "./models/track.model";
import {NavBarComponent} from "./component/nav-bar/nav-bar.component";
import {ModalComponent} from "./component/modal/modal.component";
import {AuthService} from "./service/auth.service";
import {NotificationComponent} from "./component/UI/notification/notification.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, AudioPlayerComponent, NavBarComponent, ModalComponent, NotificationComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  title = 'SmallSound';
  tracks: WritableSignal<TrackModel[] | []> = signal([])

  constructor(
    private authService: AuthService,
  ) {}

  ngOnInit(): void {

    const token = localStorage.getItem("token")

    if(token) {
      this.authService.verifyUser()
    }
  }

}
