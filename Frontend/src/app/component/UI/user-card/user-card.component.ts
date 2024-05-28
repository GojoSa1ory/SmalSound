import {Component, inject, Input} from '@angular/core';
import {UserModel} from "../../../models/user.model";
import {Router, RouterLink} from "@angular/router";
import {ModalService} from "../../../service/modal.service";

@Component({
  selector: 'app-user-card',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './user-card.component.html',
  styleUrl: './user-card.component.scss'
})
export class UserCardComponent {
  @Input() user!: UserModel
  private router = inject(Router)
  private modalService = inject(ModalService)

  navigateToUser () {
    this.router.navigateByUrl(`/user/${this.user.id}`)
    this.modalService.closeModal()
  }
}
