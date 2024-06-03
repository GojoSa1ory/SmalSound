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
  tracks:TrackModel[] | [] = []
  playlists: WritableSignal<PlaylistModel[] | []> = signal([])
  isSub: boolean = false
  listenersCount: number = 0



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
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.userId = params['id'];
      this.userSerivice.getUserProfile(this.userId)
      this.getListenersCount()
      this.getUserTracks()
      this.getUserPlaylists()
      this.verifeUser()
      this.checkSub()
    });
  }
  getUserTracks () {
    this.trackService.getAllUserTracks(this.userId).subscribe({
      next: value => {
        this.tracks = value.data!
        this.playerService.tracks.set(this.tracks)
      },
      error: err => {}
    })
  }

  getUserPlaylists () {
    this.playlisService.getUserPlaylists(this.userId).subscribe({
      next: value => {
        this.playlists.set(value.data!)
      },
      error: err => {
        this.playlists.set([])
        // this.playlisService.playlists$.set([])
      }
    })
  }

  verifeUser () {
    return this.userId == this.authService.user$()?.id!
  }

  checkSub () {
    if(!this.verifeUser()) {
      this.userSerivice.checkSub(this.userId).subscribe({
        next: value => {
          this.isSub = value.data!
        },
        error: err => {
          this.isSub = false
        }
      })
    } else {
      return
    }
  }

  getListenersCount () {
    this.userSerivice.getListenersCount(this.userId).subscribe({
      next: value => {
        this.listenersCount = value.data!
      }
    })
  }

  openSettingModal() {
    this.modalService.openModal(ChangeUserInfoComponent)
  }

  subscribeToUser() {
    this.userSerivice.subscribeToUser(this.userId)
  }

  unsubscribeFromUser() {
    this.userSerivice.unsubscribeFromUser(this.userId)
  }

}
