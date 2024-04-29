import { Routes } from '@angular/router';
import {HomeComponent} from "./pages/home/home.component";
import {UserProfileComponent} from "./pages/user-profile/user-profile.component";
import {LoginComponent} from "./pages/login/login.component";

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'profile', component: UserProfileComponent},
  {path: 'login', component: LoginComponent}
];
