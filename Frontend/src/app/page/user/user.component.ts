import {Component, OnInit, signal, WritableSignal} from '@angular/core';
import {UserService} from "../../service/user.service";
import {UserModel} from "../../models/user.model";
import {ActivatedRoute, Params, Router} from "@angular/router";

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent implements OnInit {

  userId!: number
  user: WritableSignal<UserModel | null> = signal(null)

  constructor(
    private userSerivice: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.user = this.userSerivice.user$
  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.userId = params['id'];
      this.userSerivice.getUserProfile(this.userId)
    });
  }


}
