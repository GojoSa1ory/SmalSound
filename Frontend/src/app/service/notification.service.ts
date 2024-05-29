import {Injectable, signal, WritableSignal} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private isOpen: WritableSignal<boolean> = signal(false)
  private isError: WritableSignal<boolean> = signal(false)
  private errorMessage: WritableSignal<string> = signal("")
  private message: WritableSignal<string> = signal("")

  get isOpen$ () {
    return this.isOpen
  }
  get isError$ () {
    return this.isError
  }
  get errorMessage$ () {
    return this.errorMessage
  }

  get message$ () {
    return this.message
  }

  constructor() { }

  showNotification (isError: boolean, errorMessage?: string, message?: string) {
    this.isOpen.set(true)
    this.isError.set(isError)
    if(message) this.message.set(message)
    if(errorMessage) this.errorMessage.set(errorMessage)
  }

  closeNotification () {
    this.isOpen.set(false)
    this.isError.set(false)
    this.errorMessage.set("")
    this.message.set("")
  }
}
