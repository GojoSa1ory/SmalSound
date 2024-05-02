import {Component, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import {NavBarComponent} from "./components/nav-bar/nav-bar.component";
import {AuthService} from "./service/auth.service";
import {UserService} from "./service/user.service";
import {AudioPlayerComponent} from "./components/UI/audio-player/audio-player.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavBarComponent, AudioPlayerComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  private readonly authService = inject(AuthService)
  private readonly userService = inject(UserService)

  title = 'SmallSound';
  ngOnInit(): void {

      const token: string | null | undefined = this.authService.token

      if(token != null) {
        this.userService.getProfile().subscribe({
          next: value => {
            this.authService.setUser(value.data)
            this.authService.isAuth = true
          },
          error: err => {
            console.log(err)
            localStorage.removeItem("token")
          }
        })
      }

  }
}
