import {Component, inject, OnInit} from '@angular/core';
import {TrackService} from "../../service/track.service";
import {TrackModel} from "../../models/TrackModel";
import {SetUserModel, UserModel} from "../../models/UserModel";
import {AuthService} from "../../service/auth.service";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  private readonly tracService = inject(TrackService);
  private readonly authService = inject(AuthService);

  products!: TrackModel[]

  currentUser!: UserModel

  constructor() {
    this.currentUser = this.authService.getUser()
  }

  ngOnInit(): void {
    this.getTracks()
    console.log(this.currentUser)
  }

  getTracks () {
    this.tracService.getAllTrack().subscribe({
      next: response => {
        this.products = response.data
      },
      error: err => console.log(err)
    })
  }
}
