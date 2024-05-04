import {Injectable, WritableSignal, signal} from "@angular/core";
import {TrackService} from "./track.service";
import {TrackModel} from "../models/track.model";

@Injectable({
  providedIn: "root",
})
export class AudioPlayerService {
  private audio!: HTMLAudioElement;
  private track: WritableSignal<TrackModel | null | undefined> = signal(null);
  private tracks: WritableSignal<TrackModel[] | []> = signal([]);
  private currentTime: WritableSignal<number> = signal(0);
  private currentVolume: WritableSignal<number> = signal(100);
  private duration: WritableSignal<number> = signal(0);
  // isPlaying: WritableSignal<boolean> = signal(false);
  private isPlayingState: { [key: number]: boolean } = {};

  private trackIndex: number = 0;
  private isLooping: boolean = false;

  constructor(private trackService: TrackService) {
    this.tracks = this.trackService.tracks
    this.setAudio()
  }

  setLooping() {
    this.isLooping = !this.isLooping;
  }

  getCurrentTrack() {
    return this.track;
  }

  isPlaying(track: TrackModel): boolean {
    return this.isPlayingState[track.id] || false;
  }

  getCurrentTime() {
    return this.currentTime;
  }

  getDuration() {
    return this.duration;
  }

  getCurrentVolume() {
    return this.currentVolume;
  }

  setCurrentTrack(track: TrackModel) {
    this.track.set({...track, isPlaying: true});

    //@ts-ignore
    this.trackIndex = this.trackService.tracks().indexOf(this.track());
  }

  setCurrentTime(newTime: number) {
    this.audio!.currentTime = newTime;
  }

  setCurrentVolume(volume: number) {
    this.currentVolume.set(volume);
    this.audio!.volume = this.currentVolume() / 100;
  }

  setAudio() {
    this.audio = new Audio();
    this.audio.volume = this.currentVolume() / 100;
    this.audio.ontimeupdate = () => {
      this.currentTime.set(this.audio.currentTime);
    };
  }

  play() {

    if (!this.audio || !this.track()) return;

    this.duration.set(this.audio.duration)

    this.audio!.onended = () => {
      if (this.isLooping) {
        this.audio!.currentTime = 0
        this.audio!.play()
      } else {
        this.track.set({...this.track()!})
        this.isPlayingState[this.track()!.id] = false;
      }
    }

    if (this.audio!.src !== this.track()?.track) {
      this.audio!.src = this.track()!.track
    }

    this.track.set({...this.track()!})
    this.isPlayingState[this.track()!.id] = true;
    this.audio?.play()
  }

  pause() {
    this.audio!.pause();
    this.track.set({...this.track()!});
    this.isPlayingState[this.track()!.id] = false;
  }


  stop() {
    if (this.audio) {
      this.audio.pause();
      this.audio.currentTime = 0;
      this.track.set({...this.track()!});
      this.isPlayingState[this.track()!.id] = false;
    }
  }

  nextTrack() {

    this.stop()

    if (this.isLastTrack()) {
      this.trackIndex = 0
      this.track.set(this.tracks()[0])
      this.play()
    } else {
      this.trackIndex++;
      this.track.set({...this.tracks()[this.trackIndex]})
      this.play()
    }
  }

  prevTrack() {

    this.stop()

    if (this.isFirstTrack()) {
      this.trackIndex = this.tracks().length - 1
      this.track.set(this.tracks()[this.trackIndex])
      this.play()
    } else {
      this.trackIndex--;
      this.track.set({...this.tracks()[this.trackIndex]})
      this.play()
    }

  }

  private isLastTrack() {
    return this.tracks().length > 0 && this.trackIndex >= this.tracks().length - 1
  }

  private isFirstTrack() {
    return this.tracks().length > 0 && this.trackIndex <= 0
  }
}
