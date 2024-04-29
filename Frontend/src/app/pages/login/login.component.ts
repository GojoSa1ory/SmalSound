import {Component, inject} from '@angular/core';
import {SetUserModel} from "../../models/UserModel";
import {AuthService} from "../../service/auth.service";
import {AuthFormComponent} from "../../components/UI/auth-form/auth-form.component";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    AuthFormComponent
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  username!: string
  password!: string

  private readonly authService = inject(AuthService)

  handleLogin = (event: any) => {
    event.preventDefault()

    const user: SetUserModel = {
      name: this.username,
      password: this.password
    }

    this.authService.loginUser(user).subscribe({
      next: value => {
        sessionStorage.setItem("token", value.data.token)
        this.authService.setUser(value.data.user)
      },
      error: err => console.log(err)
    })

  }

}
