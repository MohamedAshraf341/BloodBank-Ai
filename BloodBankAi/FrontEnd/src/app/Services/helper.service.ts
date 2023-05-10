import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  constructor(private http: HttpClient) { }
  getAllGov(): Observable<any[]>
  {
    return this.http.get<any[]>(`${environment.api}/Helpers/getallgovernorate`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getAllCit(): Observable<any[]>
  {
    return this.http.get<any[]>(`${environment.api}/Helpers/getallcities`).pipe(
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
}
