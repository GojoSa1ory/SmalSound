import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params } from "@angular/router";
import { PlaylistService } from "../../service/playlist.service";
import { PlaylistModel } from "../../models/playlist.model";
import { TrackCardComponent } from "../../component/UI/track-card/track-card.component";
import { AudioPlayerService } from "../../service/audio-player.service";
import { AuthService } from "../../service/auth.service";

@Component({
    selector: "app-playlist",
    standalone: true,
    imports: [TrackCardComponent],
    templateUrl: "./playlist.component.html",
    styleUrl: "./playlist.component.scss",
})
export class PlaylistComponent implements OnInit {
    playlist: PlaylistModel | null = null;

    constructor(
        private route: ActivatedRoute,
        private playlistService: PlaylistService,
        private playerSerice: AudioPlayerService,
        private authService: AuthService,
    ) {}
    ngOnInit(): void {
        this.route.params.subscribe((params: Params) => {
            this.playlistService.getPlaylist(params["id"]).subscribe({
                next: (value) => {
                    this.playlist = value.data!;
                    this.playerSerice.tracks.set(value.data!.tracks);
                },
                error: (err) => console.log(err),
            });
        });
    }
    checkUser() {
        let check: boolean = false
        if (this.playlist?.user != null || this.playlist?.user != undefined) {
          check = this.playlist?.user.id === this.authService.user$()?.id;
        }
        return check
    }
}
