import {Injectable, WritableSignal, signal} from "@angular/core";
import {TrackService} from "./track.service";
import {TrackModel} from "../models/track.model";

@Injectable({
  providedIn: "root",
})
export class AudioPlayerService {

  private audio!: HTMLAudioElement;
  private track: WritableSignal<TrackModel | null | undefined> = signal(null);
  tracks: WritableSignal<TrackModel[] | []> = signal([]);
  private currentTime: WritableSignal<number> = signal(0);
  private currentVolume: WritableSignal<number> = signal(50);
  private duration: WritableSignal<number> = signal(0);
  private isPlayingState: { [key: number]: boolean } = {};

  private trackIndex: number = 0;
  private isLooping: boolean = false;
  private isRandom: boolean = false;

  constructor(private trackService: TrackService) {
    this.tracks = this.trackService.tracks
    this.setAudio()
  }

  setLooping() {
    this.isLooping = !this.isLooping;
  }

  setRandom () {
    this.isRandom = !this.isRandom;
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
    this.trackIndex = [...this.trackService.tracks()].indexOf(this.tracks().find(t => t.id === this.track()?.id));
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
    this.audio.onloadedmetadata = () => {
      this.duration.set(this.audio.duration);
    };
  }

  play() {

    if (!this.audio || !this.track()) return;

    this.audio!.onended = () => {
      if (this.isLooping) {
        this.audio.loop = true;
        this.audio.play()
      } else if(this.isRandom){
        this.randomTrack()
      } else {
        this.audio.loop = false;
        this.stop()
        this.track.set({...this.track()!, isPlaying: false});
        this.isPlayingState[this.track()!.id] = false;
        this.nextTrack();
      }
    }

    if (this.audio!.src !== this.track()?.track) {
      this.stop()
      this.audio!.src = this.track()!.track
    }

    this.duration.set(this.audio.duration)
    this.track.set({...this.track()!, isPlaying: true})
    this.isPlayingState[this.track()!.id] = true;
    this.trackService.updateListeningAmount(this.track()!.id).subscribe()
    this.audio?.play()
  }

  pause() {
    this.audio!.pause();
    this.track.set({...this.track()!, isPlaying: false});
    this.isPlayingState[this.track()!.id] = false;
  }


  stop() {
    if (this.audio) {
      this.audio.pause();
      this.audio.currentTime = 0;
      this.track.set({...this.track()!, isPlaying: false});
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

  randomTrack() {
    this.stop()
    this.trackIndex = Math.floor(Math.random() * this.tracks().length)
    this.track.set(this.tracks()[this.trackIndex])
    this.play()
  }

  private isLastTrack() {
    return this.tracks().length > 0 && this.trackIndex >= this.tracks().length - 1
  }

  private isFirstTrack() {
    return this.tracks().length > 0 && this.trackIndex <= 0
  }
}
