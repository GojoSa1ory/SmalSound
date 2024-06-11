import { Component } from '@angular/core';
import {PlaylistService} from "../../../service/playlist.service";
import {NotificationService} from "../../../service/notification.service";

@Component({
  selector: 'app-update-playlist',
  standalone: true,
  imports: [],
  templateUrl: './update-playlist.component.html',
  styleUrl: './update-playlist.component.scss'
})
export class UpdatePlaylistComponent {

  name: string | null = null
  selectedImage: any = null
  imageUrl: string | ArrayBuffer | null = null;
  constructor(
    private playlistService: PlaylistService,
    private not: NotificationService
  ) {}

  setImage (event: any) {
    this.selectedImage = event.target.files[0]
    if (this.selectedImage) {
      const reader = new FileReader();
      reader.onload = (e) => {
        // @ts-ignore
        this.imageUrl = e.target?.result;
      };
      reader.readAsDataURL(this.selectedImage);
    }
  }

  setName (event: any) {
    this.name = event.target.value
  }

  updateInfo () {
    const request = new FormData()
    request.append("Name", this.name!)
    request.append("Image",this.selectedImage)
    this.playlistService.updatePlaylist(request).subscribe({
      next: value => {
        this.not.showNotification(false, "", "Успешное обновление данных")
        setTimeout(() => this.not.closeNotification(), 2000)
      },
      error: err => {
        this.not.showNotification(true, "Ошибка")
        setTimeout(() => this.not.closeNotification(), 2000)
      }
    })
  }

  delete () {
    this.playlistService.deletePlaylist().subscribe({
      next: value => {
        this.not.showNotification(false, "", "Успешное удаление")
        setTimeout(() => this.not.closeNotification(), 2000)
      },
      error: err => {
        this.not.showNotification(true, "Ошибка")
        setTimeout(() => this.not.closeNotification(), 2000)
      }
    })
  }


}
