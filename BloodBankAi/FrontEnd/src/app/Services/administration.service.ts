import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {apiResponse} from 'src/app/Models/ApiResponse';
import { adminstrationDto } from 'src/app/Models/Administration';
import { addAdmin } from 'src/app/Models/Administration';

@Injectable({
  providedIn: 'root'
})
export class AdministrationService {

  constructor(private http: HttpClient) { }
  getModerators(): Observable<apiResponse<adminstrationDto[]>> {
    return this.http.get<apiResponse<adminstrationDto[]>>(`${environment.api}/Adminstration/getmoderators`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  addModerator(body:addAdmin):Observable<apiResponse<addAdmin>>{
    return this.http.post<apiResponse<addAdmin>>(`${environment.api}/Adminstration/addmoderator`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  deleteModerator(userId:number):Observable<apiResponse<string>>{
    return this.http.delete<apiResponse<string>>(`${environment.api}/Adminstration/deletemoderator/${userId}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
}
