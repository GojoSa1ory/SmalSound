import {Component, OnInit, WritableSignal, signal} from '@angular/core';
import {FavoriteService} from "../../service/favorite.service";
import {FavoriteModel} from "../../models/favorite.model";
import {TrackCardComponent} from "../../component/UI/track-card/track-card.component";
import {AudioPlayerService} from "../../service/audio-player.service";
import {UserModel} from "../../models/user.model";
import {AuthService} from "../../service/auth.service";

@Component({
  selector: 'app-favorite',
  standalone: true,
  imports: [
    TrackCardComponent
  ],
  templateUrl: './favorite.component.html',
  styleUrl: './favorite.component.scss'
})
export class FavoriteComponent implements OnInit {

  favorite: WritableSignal<FavoriteModel | null> = signal(null)
  user!: WritableSignal<UserModel | null>

  constructor(
    private favoriteService: FavoriteService,
    private audioPlayerService: AudioPlayerService,
    private authService: AuthService
  ) {
    this.favorite = this.favoriteService.favorite
    this.user = this.authService.user$
  }

  ngOnInit(): void {
    this.favoriteService.getFavorite().subscribe({
      next: value => {
        this.favoriteService.favorite.set(value.data!)
        this.audioPlayerService.tracks.set(value.data!.tracks)
      },
      error: err => {

      }
    })
  }

}
