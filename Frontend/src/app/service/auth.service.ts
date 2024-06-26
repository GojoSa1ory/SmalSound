import { Injectable, signal, WritableSignal } from "@angular/core";
import { environment } from "../../environment/environment";
import { UserModel } from "../models/user.model";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";
import { AuthRequestModel, AuthResponseModel } from "../models/auth.model";
import { Subscription } from "rxjs";
import { ServerResponseModel } from "../models/serverResponse.model";
import { NotificationService } from "./notification.service";

@Injectable({
    providedIn: "root",
})
export class AuthService {
    private apiUrl: string = environment.apiUrl;
    private user: WritableSignal<UserModel | null> = signal(null);
    private isAuth: WritableSignal<boolean> = signal(false);

    get user$() {
        return this.user;
    }

    get isAuth$() {
        return this.isAuth;
    }

    constructor(
        private http: HttpClient,
        private router: Router,
        private notificationService: NotificationService,
    ) {}

    loginUser(user: AuthRequestModel): Subscription {
        return this.http
            .post<
                ServerResponseModel<AuthResponseModel>
            >(`${this.apiUrl}/auth/login`, user)
            .subscribe({
                next: (value) => {
                    localStorage.setItem("token", value.data!.token);
                    this.user.set(value.data!.user);
                    this.isAuth.set(true);
                    this.router.navigateByUrl("/");
                    this.notificationService.showNotification(
                        false,
                        "",
                        "Успешная авторизация",
                    );
                    setTimeout(() => {
                        this.notificationService.closeNotification();
                    }, 2000);
                },
                error: (err) => {
                    console.log(err);
                    localStorage.removeItem("token");
                    this.isAuth.set(false);
                    this.notificationService.showNotification(
                        true,
                        "Неверные данные пользователя",
                    );
                    setTimeout(() => {
                        this.notificationService.closeNotification();
                    }, 2000);
                },
            });
    }

    registerUser(user: AuthRequestModel): Subscription {
        return this.http
            .post<
                ServerResponseModel<AuthResponseModel>
            >(`${this.apiUrl}/auth/register`, user)
            .subscribe({
                next: (value) => {
                    localStorage.setItem("token", value.data!.token);
                    this.user.set(value.data!.user);
                    this.isAuth.set(true);
                    this.router.navigateByUrl("/");
                    this.notificationService.showNotification(
                        false,
                        "",
                        "Успешная регистрация",
                    );
                    setTimeout(() => {
                        this.notificationService.closeNotification();
                    }, 2000);
                },
                error: (err) => {
                    console.log(err);
                    localStorage.removeItem("token");
                    this.isAuth.set(false);
                    this.notificationService.showNotification(
                        true,
                        "Неверные данные",
                    );
                    setTimeout(() => {
                        this.notificationService.closeNotification();
                    }, 2000);
                },
            });
    }

    verifyUser(): Subscription {
        const headers = this.createAuthHeaders();

        return this.http
            .get<
                ServerResponseModel<UserModel>
            >(`${this.apiUrl}/auth/verify`, { headers: headers })
            .subscribe({
                next: (value) => {
                    this.user.set(value.data!);
                    this.isAuth.set(true);
                },
                error: (err) => {
                    console.log(err);
                    localStorage.removeItem("token");
                    this.isAuth.set(false);
                },
            });
    }

    logOutUser() {
        localStorage.removeItem("token");
        this.user.set(null);
        this.isAuth.set(false);
        this.router.navigateByUrl("/");
    }

    private createAuthHeaders(): HttpHeaders {
        return new HttpHeaders().set(
            "Authorization",
            `Bearer ${localStorage.getItem("token")}`,
        );
    }
}
