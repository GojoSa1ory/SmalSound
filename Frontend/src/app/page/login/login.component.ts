import { Component } from '@angular/core';
import {AuthFormComponent} from "../../component/auth-form/auth-form.component";

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

}
