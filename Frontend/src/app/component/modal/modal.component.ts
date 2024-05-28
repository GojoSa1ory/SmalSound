import {
  Component,
  WritableSignal, signal
} from '@angular/core';
import {ModalService} from "../../service/modal.service";
import {NgComponentOutlet} from "@angular/common";
import {LucideAngularModule} from "lucide-angular";

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [
    NgComponentOutlet,
    LucideAngularModule
  ],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss'
})
export class ModalComponent {

  isVisible: WritableSignal<boolean> = signal(false);
  content: WritableSignal<any> = signal(null);

  constructor(private modalService: ModalService) {
    this.isVisible = this.modalService.modalVisibility$
    this.content = this.modalService.modalContent$
  }

  loadComponent() {
    return this.content()
  }

  close() {
    this.modalService.closeModal()
  }

}
