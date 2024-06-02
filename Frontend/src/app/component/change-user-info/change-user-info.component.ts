import { Component } from '@angular/core';
import {UserService} from "../../service/user.service";

@Component({
  selector: 'app-change-user-info',
  standalone: true,
  imports: [],
  templateUrl: './change-user-info.component.html',
  styleUrl: './change-user-info.component.scss'
})
export class ChangeUserInfoComponent {

  name: string | null = null
  password: string | null = null


  selectedImage: any = null
  imageUrl: string | ArrayBuffer | null = null;

  constructor(private userService: UserService) {}
  setUserImage (event: any) {
    this.selectedImage = event.target.files[0]
    if (this.selectedImage) {
      const reader = new FileReader();
      reader.onload = (e) => {
        // @ts-ignore
        this.imageUrl = e.target?.result;
      };
      reader.readAsDataURL(this.selectedImage);
    }
  }

  setUserName (event: any) {
    this.name = event.target.value
  }

  setPassword (event: any) {
    this.password = event.target.value
  }

  updateUserInfo() {
    const request = new FormData()
    request.append("Password",this.password!)
    request.append("Name", this.name!)
    request.append("ProfilePicture",this.selectedImage)

    this.userService.updateUserInfo(request)
  }
}
