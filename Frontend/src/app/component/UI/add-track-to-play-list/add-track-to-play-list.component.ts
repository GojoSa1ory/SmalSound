import {Component, Input, signal, WritableSignal} from '@angular/core';
import {PlaylistModel} from "../../../models/playlist.model";
import {PlaylistService} from "../../../service/playlist.service";
import {NotificationService} from "../../../service/notification.service";

@Component({
  selector: 'app-add-track-to-play-list',
  standalone: true,
  imports: [],
  templateUrl: './add-track-to-play-list.component.html',
  styleUrl: './add-track-to-play-list.component.scss'
})
export class AddTrackToPlayListComponent {
  playlists: WritableSignal<PlaylistModel[] | []> = signal([])
  trackId!: WritableSignal<number>

  constructor(
    private playlistService: PlaylistService,
    private notificationService: NotificationService
  ) {
    this.playlists = this.playlistService.playlists$
    this.trackId = this.playlistService.trackId
  }

  addToPlaylist (playlistId: number) {
    this.playlistService.addTrackToPlayList(playlistId, this.trackId()).subscribe({
      next: () => {
        this.notificationService.showNotification(false, "", "Успешно")
        setTimeout(() => this.notificationService.closeNotification(), 2000)
      },
      error: err => {
        this.notificationService.showNotification(true, "Ошибка")
        setTimeout(() => this.notificationService.closeNotification(), 2000)
      }
    })
  }
}
