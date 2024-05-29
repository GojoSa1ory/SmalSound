import {ApplicationConfig, importProvidersFrom} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {provideHttpClient, withFetch} from "@angular/common/http";
import {
  LucideAngularModule,
  Home,
  Search,
  ShieldCheck,
  UserPlus,
  CircleUser,
  Heart,
  MicVocal,
  Volume2,
  Play,
  Pause,
  ChevronFirst,
  ChevronLast,
  Repeat,
  Shuffle,
  LogOut,
  BookCheck,
  X,
  Download,
  SquarePlus,
  Ellipsis,
} from "lucide-angular";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withFetch()),
    importProvidersFrom((LucideAngularModule.pick({
      Home,
      Search,
      ShieldCheck,
      UserPlus,
      CircleUser,
      Heart,
      MicVocal,
      Volume2,
      Play,
      Pause,
      ChevronFirst,
      ChevronLast,
      Repeat,
      Shuffle,
      LogOut,
      BookCheck,
      X,
      Download,
      SquarePlus,
      Ellipsis,
    })))]
};
