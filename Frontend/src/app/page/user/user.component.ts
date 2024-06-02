import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import {UserService} from "../../service/user.service";
import {UserModel} from "../../models/user.model";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {TrackService} from "../../service/track.service";
import {TrackModel} from "../../models/track.model";
import {TrackCardComponent} from "../../component/UI/track-card/track-card.component";
import {AuthService} from "../../service/auth.service";
import {AudioPlayerService} from "../../service/audio-player.service";
import {PlaylistService} from "../../service/playlist.service";
import {PlaylistModel} from "../../models/playlist.model";
import {PlaylistCardComponent} from "../../component/UI/playlist-card/playlist-card.component";
import {ModalService} from "../../service/modal.service";
import {ChangeUserInfoComponent} from "../../component/change-user-info/change-user-info.component";

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    TrackCardComponent,
    PlaylistCardComponent
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent implements OnInit {

  userId!: number
  user: WritableSignal<UserModel | null> = signal(null)
  tracks: WritableSignal<TrackModel[] | []> = signal([])
  playlists: WritableSignal<PlaylistModel[] | []> = signal([])



  constructor(
    private userSerivice: UserService,
    private trackService: TrackService,
    private authService: AuthService,
    private playerService: AudioPlayerService,
    private playlisService: PlaylistService,
    private modalService: ModalService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.user = this.userSerivice.user$
    this.tracks = this.trackService.userTracks
    this.playlists = this.playlisService.playlists$
  }

  ngOnInit(): void {
    this.setUpPage()
  }

  setUpPage () {
    this.getUser()
    this.getUserTracks()
    this.getUserPlaylists()
    this.verifeUser()

  }

  getUser() {
    this.route.params.subscribe((params: Params) => {
      this.userId = params['id'];
      this.userSerivice.getUserProfile(this.userId)
    });
  }

  getUserTracks () {
    this.trackService.getAllUserTracks(this.userId).subscribe({
      next: value => {
        this.trackService.userTracks.set(value.data!)
        this.playerService.tracks.set(value.data!)
      },
      error: err => {}
    })
  }

  getUserPlaylists () {
    this.playlisService.getUserPlaylists(this.userId).subscribe({
      next: value => {
        this.playlisService.playlists$.set(value.data!)
      },
      error: err => {}
    })
  }

  verifeUser () {
    return this.userId == this.authService.user$()?.id!
  }

  openSettingModal() {
    this.modalService.openModal(ChangeUserInfoComponent)
  }

  subscribeToUser() {

  }

}
