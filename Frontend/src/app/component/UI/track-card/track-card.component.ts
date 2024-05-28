import {Component, Input, OnInit, WritableSignal} from '@angular/core';
import {LucideAngularModule} from "lucide-angular";
import {RouterLink} from "@angular/router";
import {TrackModel} from "../../../models/track.model";
import {AudioPlayerService} from "../../../service/audio-player.service";

@Component({
  selector: 'app-track-card',
  standalone: true,
  imports: [
    LucideAngularModule,
    RouterLink
  ],
  templateUrl: './track-card.component.html',
  styleUrl: './track-card.component.scss'
})
export class TrackCardComponent{

  @Input() track!: TrackModel
  // isPlaying: boolean = false

  constructor(protected audioService: AudioPlayerService) {
  }
  handlePlay () {
   if(this.isPlaying()) {
     this.audioService.pause()
   } else {
     this.audioService.stop()
     this.audioService.setCurrentTrack(this.track!)
     this.audioService.play()
   }
  }

  isPlaying(): boolean {
    return this.audioService.isPlaying(this.track);
  }

}
