import {Component, inject, Input, WritableSignal} from '@angular/core';
import {NotificationService} from "../../../service/notification.service";

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.scss'
})
export class NotificationComponent {

  private notificationService: NotificationService = inject(NotificationService)

  isOpen: WritableSignal<boolean> = this.notificationService.isOpen$
  isError: WritableSignal<boolean> = this.notificationService.isError$
  errorMessage: WritableSignal<string> = this.notificationService.errorMessage$
  message: WritableSignal<string> = this.notificationService.message$

}
