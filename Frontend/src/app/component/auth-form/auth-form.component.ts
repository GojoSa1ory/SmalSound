import {Component, Input} from '@angular/core';
import {AuthRequestModel} from "../../models/auth.model";
import {UserService} from "../../service/user.service";

@Component({
  selector: 'app-auth-form',
  standalone: true,
  imports: [],
  templateUrl: './auth-form.component.html',
  styleUrl: './auth-form.component.scss'
})
export class AuthFormComponent {

  user: AuthRequestModel = {
    name: "",
    email: "",
    password: ""
  }

  @Input() isReg: boolean = false;

  constructor(private userService: UserService) {
  }

  handleSetCurrentName (event: Event) {
    const newName = (event.target as HTMLInputElement).value
    this.user = {...this.user, name: newName}
  }

  handleSetCurrentEmail (event: Event) {
    const newEmail = (event.target as HTMLInputElement).value
    this.user = {...this.user, email: newEmail}
  }

  handleSetCurrentPassword (event: Event) {
    const newPassword = (event.target as HTMLInputElement).value
    this.user = {...this.user, password: newPassword}
  }

  handleClick (event: Event) {
    event.preventDefault()

    this.isReg ? this.userService.registerUser(this.user).subscribe({
      next: value => {
        localStorage.setItem("token", value.data!.token)
        this.userService.user.set(value.data!.user)
        this.userService.isAuth.set(true)
      },
      error: err => console.log(err)
    }) : this.userService.loginUser(this.user).subscribe({
      next: value => {
        localStorage.setItem("token", value.data!.token)
        this.userService.user.set(value.data!.user)
        this.userService.isAuth.set(true)
      },
      error: err => console.log(err)
    })
  }


}
