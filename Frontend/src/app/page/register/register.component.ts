import {Component} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {AuthFormComponent} from "../../component/auth-form/auth-form.component";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    AuthFormComponent
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
}
