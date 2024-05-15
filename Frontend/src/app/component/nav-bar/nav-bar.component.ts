import {Component, signal, WritableSignal} from '@angular/core';
import {RouterLink} from "@angular/router";
import {LucideAngularModule, Home} from "lucide-angular";
import {UserService} from "../../service/user.service";
import {UserModel} from "../../models/user.model";
import {SIGNAL} from "@angular/core/primitives/signals";

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [
    RouterLink,
    LucideAngularModule
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent {
  isAuth!: WritableSignal<boolean>
  user: WritableSignal<UserModel | null | undefined> = signal(null)
  constructor(private userService: UserService) {
    this.isAuth = this.userService.isAuth
    this.user = this.userService.user
  }

}
