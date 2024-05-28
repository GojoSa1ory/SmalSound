import {Component, signal, WritableSignal} from '@angular/core';
import {RouterLink} from "@angular/router";
import {LucideAngularModule} from "lucide-angular";
import {UserModel} from "../../models/user.model";
import {ModalService} from "../../service/modal.service";
import {AuthService} from "../../service/auth.service";

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
  constructor(
    private authService: AuthService,
    private modalService: ModalService
  ) {
    this.isAuth = this.authService.isAuth$
    this.user = this.authService.user$
  }

  handleLogOut () {
    this.authService.logOutUser()
  }

}
