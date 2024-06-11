import {
    Component,
    Input,
    OnInit,
    signal,
    WritableSignal,
} from "@angular/core";
import { LucideAngularModule } from "lucide-angular";
import { RouterLink } from "@angular/router";
import { TrackModel } from "../../../models/track.model";
import { AudioPlayerService } from "../../../service/audio-player.service";
import { FavoriteService } from "../../../service/favorite.service";
import { NotificationService } from "../../../service/notification.service";
import { TrackService } from "../../../service/track.service";
import { PlaylistService } from "../../../service/playlist.service";
import { ModalService } from "../../../service/modal.service";
import { AddTrackToPlayListComponent } from "../add-track-to-play-list/add-track-to-play-list.component";
import { UserModel } from "../../../models/user.model";
import { AuthService } from "../../../service/auth.service";

@Component({
    selector: "app-track-card",
    standalone: true,
    imports: [LucideAngularModule, RouterLink],
    templateUrl: "./track-card.component.html",
    styleUrl: "./track-card.component.scss",
})
export class TrackCardComponent {
    @Input() track!: TrackModel;
    @Input() page: "playlist" | "favorite" | "userPublication" | "none" =
        "none";
    @Input() playlistId!: number;
    @Input() isVerifided!: boolean;

    constructor(
        protected audioService: AudioPlayerService,
        private favoriteService: FavoriteService,
        private notification: NotificationService,
        private trackService: TrackService,
        private playlistService: PlaylistService,
        private modalService: ModalService,
    ) {}
    handlePlay() {
        if (this.isPlaying()) {
            this.audioService.pause();
        } else {
            this.audioService.stop();
            this.audioService.setCurrentTrack(this.track!);
            this.audioService.play();
        }
    }

    addTrackToFavorite() {
        this.favoriteService.addTrackToFavorite(this.track.id).subscribe({
            next: (value) => {
                this.notification.showNotification(
                    false,
                    "",
                    "Избранное успешно обновлено",
                );
                setTimeout(() => this.notification.closeNotification(), 2000);
            },
            error: (err) => {
                this.notification.showNotification(
                    true,
                    "Ошибка при изменении избранного",
                );
                setTimeout(() => this.notification.closeNotification(), 2000);
            },
        });
    }

    removeFromPlaylist() {
        if (this.page === "userPublication") {
            console.log("click");
            this.trackService.deleteTrack(this.track.id).subscribe({
                next: (value: any) => {
                    this.notification.showNotification(
                        false,
                        "",
                        "Трек успешно удален",
                    );
                    setTimeout(
                        () => this.notification.closeNotification(),
                        2000,
                    );
                },
                error: (err) => {
                    this.notification.showNotification(
                        true,
                        "Ошибка при удалении",
                        "",
                    );
                    setTimeout(
                        () => this.notification.closeNotification(),
                        2000,
                    );
                },
            });
        } else if (this.page === "playlist") {
            this.playlistService
                .deleteTrackFormPlaylist(this.playlistId, this.track.id)
                .subscribe({
                    next: (value: any) => {
                        this.playlistService.playlist$.set(value.data);
                        this.notification.showNotification(
                            false,
                            "",
                            "Успешно",
                        );
                        setTimeout(
                            () => this.notification.closeNotification(),
                            2000,
                        );
                    },
                    error: (err) => {
                        this.notification.showNotification(true, "Ошибка", "");
                        setTimeout(
                            () => this.notification.closeNotification(),
                            2000,
                        );
                    },
                });
        } else if (this.page === "favorite") {
            this.addTrackToFavorite();
        }
    }

    openModal() {
        this.playlistService.trackId.set(this.track.id);
        this.modalService.openModal(AddTrackToPlayListComponent);
    }

    isPlaying(): boolean {
        return this.audioService.isPlaying(this.track);
    }
}
