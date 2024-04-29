import {Component, inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import {NavBarComponent} from "./components/nav-bar/nav-bar.component";
import {FooterComponent} from "./components/footer/footer.component";
import {AuthService} from "./service/auth.service";
import {UserService} from "./service/user.service";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NavBarComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  private readonly authService = inject(AuthService)
  private readonly userService = inject(UserService)

  ngOnInit(): void {

      const token = localStorage.getItem("token")

      if(token) {
        this.userService.getProfile().subscribe({
          next: value => {
            this.authService.setUser(value.data)
          },
          error: err => console.log(err)
        })
      }
  }
  title = 'SmallSound';


}
