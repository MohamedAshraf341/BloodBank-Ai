import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {apiResponse} from 'src/app/Models/ApiResponse';
import{getDonors}from 'src/app/Models/Donors';
import{getDonorById}from 'src/app/Models/Donors';

@Injectable({
  providedIn: 'root'
})
export class FindDonorsService {
   
  constructor(private http: HttpClient) { }
  getAllDonors(): Observable<apiResponse<getDonors[]>> {
    return this.http.get<apiResponse<getDonors[]>>(`${environment.api}/Donor/getalldonors`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getDonorById(userId:string): Observable<apiResponse<getDonorById>> {
    return this.http.get<apiResponse<getDonorById>>(`${environment.api}/Donor/getdonorbyid/${userId}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
}
