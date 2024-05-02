import { Component } from '@angular/core';
import {RouterLink} from "@angular/router";
import {UserService} from "../../service/user.service";
import {AuthService} from "../../service/auth.service";
import {LibraryLinks, MenuLinks} from "../../constants/NavLinks";

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: 'nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent {

    protected readonly Component = Component;
    constructor(private readonly authService: AuthService) {}

    protected readonly isAuth = this.authService.isAuth;
    protected readonly menuLinks = MenuLinks
    protected readonly libraryLinks = LibraryLinks


}
