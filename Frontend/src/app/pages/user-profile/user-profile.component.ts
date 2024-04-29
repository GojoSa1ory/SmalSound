import {Component, inject} from '@angular/core';
import {AuthService} from "../../service/auth.service";
import {UserModel} from "../../models/UserModel";

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent {

  private readonly authService = inject(AuthService)

  user!: UserModel
  constructor() {
    this.user = this.authService.getUser()
  }

}
