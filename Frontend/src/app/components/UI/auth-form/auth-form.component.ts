import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-auth-form',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './auth-form.component.html',
  styleUrl: './auth-form.component.scss'
})
export class AuthFormComponent {

  @Input() username: string = ""
  @Output() usernameChange = new EventEmitter<string>()

  @Input() password: string = ""
  @Output() passwordChange = new EventEmitter<string>()

  @Input() email: string = ""
  @Output() emailChange = new EventEmitter<string>()

  @Input() isLogginPage: boolean = false

  @Input() handleClick!: any

  onNameChange(name: string){
    this.username = name;
    this.usernameChange.emit(name);
  }

  onEmailChange(email: string){
    this.email = email;
    this.emailChange.emit(email);
  }

  onPasswordChange(password: string){
    this.password = password;
    this.passwordChange.emit(password);
  }
}
