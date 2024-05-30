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
import {PlaylistComponent} from "./page/playlist/playlist.component";
import {FavoriteComponent} from "./page/favorite/favorite.component";

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
    component: UserPublicationsComponent,
    canActivate: [userAuthCheckGuard]
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
    component: UploadTrackComponent,
    canActivate: [userAuthCheckGuard]
  },
  {
    path: "playlists/:id",
    component: PlaylistComponent
  },
  {
    path: "favorites",
    component: FavoriteComponent,
    canActivate: [userAuthCheckGuard]
  },
  {
    path: "**",
    component: NotFoundComponent
  }
];
