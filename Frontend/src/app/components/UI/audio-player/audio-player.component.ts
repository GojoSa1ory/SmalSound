import { Component, WritableSignal } from "@angular/core";
import { AudioPlayerService } from "../../../service/audio-player.service";
import { TrackModel } from "../../../models/TrackModel";
import { FormsModule } from "@angular/forms";
import { Subscription } from "rxjs";

@Component({
    selector: "app-audio-player",
    standalone: true,
    imports: [FormsModule],
    templateUrl: "./audio-player.component.html",
    styleUrl: "./audio-player.component.scss",
})
export class AudioPlayerComponent {
    track!: WritableSignal<TrackModel | null>;
    currentTime!: WritableSignal<number>;
    duration!: WritableSignal<number>;
    currentVolume!: WritableSignal<number>;

    constructor(protected playerService: AudioPlayerService) {
        this.track = this.playerService.getCurrentTrack();
        this.currentTime = this.playerService.getCurrentTime();
        this.duration = this.playerService.getDuration();
        this.currentVolume = this.playerService.getCurrentVolume();
    }

    handleLoop() {
        this.playerService.setLooping();
    }

    handleChangeCurrentTime(event: Event) {
        const newTime = (event.target as HTMLInputElement).valueAsNumber;
        this.currentTime.set(newTime);
        this.playerService.setCurrentTime();
    }

    setCurrentVolume(event: Event) {
        const newVolume = (event.target as HTMLInputElement).valueAsNumber;
        console.log(newVolume);
        this.playerService.setCurrentVolume(newVolume);
    }

    handleClick() {
        if (this.track()?.isPlaying) {
            this.playerService.pause();
        } else {
            this.playerService.play();
        }
    }
}
