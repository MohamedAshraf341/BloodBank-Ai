import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {apiResponse} from 'src/app/Models/ApiResponse';
import { loginModel } from 'src/app/Models/Acount';
import { responseAuth } from 'src/app/Models/Acount';
import { registerModel } from 'src/app/Models/Acount';
import{RevokeTokenDto}from 'src/app/Models/Acount';
import { NavigationExtras, Router } from '@angular/router';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class AcountService {
  private tokenKey = 'jwt_token';
  private userKey = 'user';  
  constructor(private http: HttpClient) { }
  login(body:loginModel): Observable<apiResponse<responseAuth>> {
    return this.http.post<apiResponse<responseAuth>>(`${environment.api}/Account/login`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  register(body:registerModel): Observable<apiResponse<responseAuth>> {
    return this.http.post<apiResponse<responseAuth>>(`${environment.api}/Account/register`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  clearToken(): void {
    localStorage.removeItem(this.tokenKey);
  }

  setUser(user: any): void {
    localStorage.setItem(this.userKey, JSON.stringify(user));
  }

  getUser(): any {
    const userStr = localStorage.getItem(this.userKey);
    return userStr ? JSON.parse(userStr) : null;
  }

  clearUser(): void {
    localStorage.removeItem(this.userKey);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token;
  }

  getUserRoles(): string[] {
    const user = this.getUser();
    return user ? user.roles : [];
  }
}
