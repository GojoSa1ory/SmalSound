import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import {RouterLink} from "@angular/router";
import {LucideAngularModule} from "lucide-angular";
import {UserModel} from "../../models/user.model";
import {ModalService} from "../../service/modal.service";
import {AuthService} from "../../service/auth.service";
import {PlaylistService} from "../../service/playlist.service";
import {NotificationService} from "../../service/notification.service";
import {PlaylistModel} from "../../models/playlist.model";

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [
    RouterLink,
    LucideAngularModule
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent implements OnInit {

  isAuth!: WritableSignal<boolean>
  user: WritableSignal<UserModel | null | undefined> = signal(null)
  playlists: WritableSignal<PlaylistModel[] | []> = signal([])

  constructor(
    private authService: AuthService,
    private modalService: ModalService,
    private notificationService: NotificationService,
    private playlistService: PlaylistService
  ) {
    this.isAuth = this.authService.isAuth$
    this.user = this.authService.user$
    this.playlists = this.playlistService.playlists$
  }

  ngOnInit(): void {
    setTimeout(() => this.getUserPlaylists(), 2000)
  }

  getUserPlaylists() {
    this.playlistService.getUserPlaylists(this.user()!.id).subscribe({
      next: value => {
        this.playlistService.playlists$.set(value.data!)
      }
    })
  }

  handleLogOut() {
    this.authService.logOutUser()
  }

  createPlaylist() {
    this.playlistService.createPlaylist().subscribe({
      next: value => {
        this.notificationService.showNotification(false, "", "Плейлист создан")
        setTimeout(() => this.notificationService.closeNotification(), 2000)
      },
      error: err => {
        this.notificationService.showNotification(true, "Ошибка при создании плейлиста")
        setTimeout(() => this.notificationService.closeNotification(), 2000)
      }
    })
  }

}
