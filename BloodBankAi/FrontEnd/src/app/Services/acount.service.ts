import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {apiResponse} from 'src/app/Models/ApiResponse';
import { loginModel } from 'src/app/Models/Acount';
import { responseAuth } from 'src/app/Models/Acount';
import { register } from 'src/app/Models/Acount';

@Injectable({
  providedIn: 'root'
})
export class AcountService {

  constructor(private http: HttpClient) { }
  login(body:loginModel):Observable<apiResponse<responseAuth>>{
    return this.http.post<apiResponse<responseAuth>>(`${environment.api}/Account/login`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  register(body:register):Observable<apiResponse<responseAuth>>{
    return this.http.post<apiResponse<responseAuth>>(`${environment.api}/Account/register`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  refershToken():Observable<apiResponse<responseAuth>>{
    return this.http.get<apiResponse<responseAuth>>(`${environment.api}/Account/refreshToken`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
}
