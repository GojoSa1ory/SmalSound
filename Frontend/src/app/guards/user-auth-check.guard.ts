import { CanActivateFn, Router } from '@angular/router';
import {inject, WritableSignal} from "@angular/core";
import {AuthService} from "../service/auth.service";

export const userAuthCheckGuard: CanActivateFn = (route, state) => {

  const router = inject(Router)
  const authService = inject(AuthService)

  const token = localStorage.getItem("token")
  const isAuht: WritableSignal<boolean> = authService.isAuth$

  if(token === null && !isAuht()) {
    router.navigateByUrl("/login")
    return false;
  } else {
    return true;
  }

};
