import {Component, WritableSignal} from '@angular/core';
import {UserModel} from "../../models/user.model";
import {UserService} from "../../service/user.service";

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  user!: WritableSignal<UserModel | null>

  constructor(private userService: UserService) {
    this.user = this.userService.user
  }


}
