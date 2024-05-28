import { Routes } from '@angular/router';
import {HomeComponent} from "./page/home/home.component";
import {LoginComponent} from "./page/login/login.component";
import {RegisterComponent} from "./page/register/register.component";
import {userAuthCheckGuard} from "./guards/user-auth-check.guard";
import {UserPublicationsComponent} from "./page/user-publications/user-publications.component";
import {NotFoundComponent} from "./page/not-found/not-found.component";
import {UserComponent} from "./page/user/user.component";
import {SearchComponent} from "./page/search/search.component";
import {UploadTrackComponent} from "./page/upload-track/upload-track.component";

export const routes: Routes = [
  {
    path: "",
    component: HomeComponent
  },
  {
    path: "login",
    component: LoginComponent,
  },
  {
    path: "register",
    component: RegisterComponent
  },
  {
    path: "myPublications",
    component: UserPublicationsComponent
  },
  {
    path: "user/:id",
    loadComponent: () => import("./page/user/user.component").then(c => c.UserComponent)
    // canActivate: [userAuthCheckGuard]
  },
  {
    path: "search",
    component: SearchComponent
  },
  {
    path: "uploadTrack",
    component: UploadTrackComponent
  },
  {
    path: "**",
    component: NotFoundComponent
  }
];
