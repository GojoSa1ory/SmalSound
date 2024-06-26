import {Component, WritableSignal, signal} from "@angular/core";
import {TrackModel} from "../../models/track.model";
import {AudioPlayerService} from "../../service/audio-player.service";
import { LucideAngularModule } from "lucide-angular";

@Component({
  selector: "app-audio-player",
  standalone: true,
  imports: [
    LucideAngularModule
  ],
  templateUrl: "./audio-player.component.html",
  styleUrl: "./audio-player.component.scss",
})
export class AudioPlayerComponent {
  track: WritableSignal<TrackModel | null | undefined> = signal(null);
  currentTime: WritableSignal<number> = signal(0);
  currentVolume: WritableSignal<number> = signal(0.6);
  duration: WritableSignal<number> = signal(0);

  constructor(private audioService: AudioPlayerService) {
    this.track = this.audioService.getCurrentTrack();
    this.currentTime = this.audioService.getCurrentTime();
    this.currentVolume = this.audioService.getCurrentVolume();
    this.duration = this.audioService.getDuration();
  }

  handlePlay() {
    if (this.isPlaying()) {
      this.audioService.pause()
    } else {
      this.audioService.play();
    }
  }

  handlePrevTrack() {
    this.audioService.prevTrack()
  }

  isPlaying(): boolean {
    return this.audioService.isPlaying(this.track()!);
  }

  handleNextTrack() {
    this.audioService.nextTrack()
  }

  handleLoop() {
    this.audioService.setLooping()
  }

  handleRandomTrack () {
    this.audioService.setRandom()
  }

  handleSetTime(event: Event) {
    const newTime = (event.target as HTMLInputElement).valueAsNumber;
    this.audioService.setCurrentTime(newTime);
  }

  handleSetCurrentVolume(event: Event) {
    const newVolume = (event.target as HTMLInputElement).valueAsNumber;
    this.audioService.setCurrentVolume(newVolume);
  }

}
