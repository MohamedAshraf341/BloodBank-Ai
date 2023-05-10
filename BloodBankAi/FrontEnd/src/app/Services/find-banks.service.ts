import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import{HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {apiResponse} from 'src/app/Models/ApiResponse';
import{bankWithBloodGroups}from 'src/app/Models/Banks';
import{bankByIdWithAddress}from 'src/app/Models/Banks';

@Injectable({
  providedIn: 'root'
})
export class FindBanksService {

  constructor(private http: HttpClient) { }
  getAllBanks(): Observable<apiResponse<bankWithBloodGroups[]>> {
    return this.http.get<apiResponse<bankWithBloodGroups[]>>(`${environment.api}/Bank/getallbanks`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getBankById(bankId:number): Observable<apiResponse<bankByIdWithAddress>> {
    return this.http.get<apiResponse<bankByIdWithAddress>>(`${environment.api}/Bank/getbankbyid/${bankId}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
}
