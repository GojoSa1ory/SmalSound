import { Injectable, signal, WritableSignal } from "@angular/core";
import { TrackModel } from "../models/TrackModel";
import { Writable } from "node:stream";

@Injectable({
    providedIn: "root",
})
export class AudioPlayerService {
    private audio: HTMLAudioElement | null = null;
    private track: WritableSignal<TrackModel | null> = signal(null);
    private isLooping: boolean = false;
    private currentTime: WritableSignal<number> = signal(0);
    private duration: WritableSignal<number> = signal(0);
    private currentVolume: WritableSignal<number> = signal(100);

    constructor() {}

    setAudio(audio: HTMLAudioElement) {
        this.audio = audio;
        this.audio.volume = this.currentVolume() / 100;
        this.audio.addEventListener("timeupdate", () => {
            this.currentTime.set(this.audio!.currentTime);
            this.duration.set(this.audio!.duration);
        });
    }

    setTrack(track: TrackModel) {
        this.track.set(track);
    }

    setLooping() {
        this.isLooping = !this.isLooping;
    }

    getCurrentTrack(): WritableSignal<TrackModel | null> {
        return this.track;
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

    setCurrentTime() {
        try {

          this.audio!.currentTime = 20;

        } catch (e) {
          console.log(e)
        }
    }

    setCurrentVolume(volume: number) {
        this.currentVolume.set(volume);
        this.audio!.volume = this.currentVolume() / 100;
    }

    play() {
        if (!this.audio || !this.track()) return;

        this.duration.set(this.audio.duration);

        this.audio!.onended = () => {
            if (this.isLooping) {
                this.audio!.currentTime = 0;
                this.audio!.play();
            } else {
                this.track.set({ ...this.track()!, isPlaying: false });
            }
        };

        if (this.audio!.src !== this.track()?.track) {
            this.audio!.src = this.track()!.track;
        }

        this.track.set({ ...this.track()!, isPlaying: true });
        this.audio?.play();
    }

    pause() {
        this.audio!.pause();
        this.track.set({ ...this.track()!, isPlaying: false });
        this.currentTime.set(0);
    }

    stop() {
        if (this.audio) {
            this.audio.pause();
            this.audio.currentTime = 0;
            this.track.set({ ...this.track()!, isPlaying: false });
            this.currentTime.set(0);
        }
    }
}
