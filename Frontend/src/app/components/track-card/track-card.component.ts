import {Component, Input, OnInit} from '@angular/core';
import {TrackModel} from "../../models/TrackModel";
import {AudioPlayerService} from "../../service/audio-player.service";

@Component({
  selector: 'app-track-card',
  standalone: true,
  templateUrl: './track-card.component.html',
  styleUrl: './track-card.component.scss'
})
export class TrackCardComponent{

  @Input() track!: TrackModel
  audio!: HTMLAudioElement

  constructor(private audioService: AudioPlayerService) {}

  togglePlayPause(): void {

    this.audio = new Audio()

    if(this.track.isPlaying){
      this.audioService.pause()
    } else {
      this.audioService.stop()
      this.audio.preload = "none"
      this.audioService.setAudio(this.audio)
      this.audioService.setTrack(this.track)
      this.audioService.play()
    }
  }

}
