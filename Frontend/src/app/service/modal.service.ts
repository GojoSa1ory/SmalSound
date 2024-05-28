import {Injectable, signal, WritableSignal} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  private modalVisibility: WritableSignal<boolean> = signal(false);
  private modalContent: WritableSignal<any> = signal(null);

  constructor() { }

  get modalVisibility$() {
    return this.modalVisibility;
  }

  get modalContent$() {
    return this.modalContent;
  }

  openModal(component: any) {
    this.modalContent.set(component);
    this.modalVisibility.set(true);
  }

  closeModal() {
    this.modalVisibility.set(false);
    this.modalContent.set(null);
  }
}
