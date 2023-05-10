import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {apiResponse} from 'src/app/Models/ApiResponse';
import {banks} from 'src/app/Models/Admin'
import {bank} from 'src/app/Models/Admin'
import { addModerator } from 'src/app/Models/Admin';
import { addbank } from 'src/app/Models/Admin';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }
  getAllBanks(): Observable<apiResponse<banks[]>> {
    return this.http.get<apiResponse<banks[]>>(`${environment.api}/Admin/getallbanks`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getBank(id:number): Observable<apiResponse<bank>> {
    return this.http.get<apiResponse<bank>>(`${environment.api}/Admin/getbankbyid/${id}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  updateBank(body:any,bankId:number):Observable<apiResponse<string>>{
    return this.http.put<apiResponse<string>>(`${environment.api}/Admin/updatebankb/${bankId}`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  addModerator(body:addModerator):Observable<apiResponse<string>>{
    return this.http.post<apiResponse<string>>(`${environment.api}/Admin/addmoderator`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  addBank(body:any):Observable<apiResponse<addbank>>{
    return this.http.post<apiResponse<addbank>>(`${environment.api}/Admin/addbankb`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  deleteModerator(userId:number):Observable<apiResponse<string>>{
    return this.http.delete<apiResponse<string>>(`${environment.api}/Admin/deletemoderator/${userId}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
}
