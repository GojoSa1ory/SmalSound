import {Component, Input} from '@angular/core';
import {PlaylistModel} from "../../../models/playlist.model";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-playlist-card',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './playlist-card.component.html',
  styleUrl: './playlist-card.component.scss'
})
export class PlaylistCardComponent {
  @Input() playlist!: PlaylistModel
}
