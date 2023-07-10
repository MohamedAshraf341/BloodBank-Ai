import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { requestAi,responsAi } from '../Models/AiModel';
import { apiResponse } from '../Models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  constructor(private http: HttpClient) { }
  getBloodGroups(): Observable<any[]>
  {
    return this.http.get<any[]>(`${environment.api}/Helpers/getBloodGroups`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getAllGov(): Observable<any[]>
  {
    return this.http.get<any[]>(`${environment.api}/Helpers/getallgovernorate`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getAllCit(id:number): Observable<any[]>
  {
    return this.http.get<any[]>(`${environment.api}/Helpers/getallcitiesByGovernId/${id}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getgovernorate(id:number): Observable<any>
  {
    return this.http.get<any>(`${environment.api}/Helpers/getgovernorate/${id}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getAllModerator(): Observable<any[]>
  {
    return this.http.get<any[]>(`${environment.api}/Helpers/getTypeModerators`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getAllAdmin(): Observable<any[]>
  {
    return this.http.get<any[]>(`${environment.api}/Helpers/getTypeAdmins`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getNews(): Observable<any> {
    return this.http.get<any>(`${environment.api}/Helpers/getNews`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getAiPrediction(body:requestAi): Observable<apiResponse<responsAi>> {
    return this.http.post<apiResponse<responsAi>>(`${environment.api}/AiApi/test`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
}
