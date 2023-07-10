import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { addModerator, bank, banks } from '../Models/Admin';
import { apiResponse } from '../Models/ApiResponse';
import { BloodGroupUpdateDto } from '../Models/Moderator';
@Injectable({
  providedIn: 'root'
})
export class ModeratorService {

  constructor(private http: HttpClient) { }
  getAllBanks(id:string): Observable<apiResponse<banks[]>> {
    return this.http.get<apiResponse<banks[]>>(`${environment.api}/BankAdmin/getallbanks/${id}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getBank(id:number): Observable<apiResponse<bank>> {
    return this.http.get<apiResponse<bank>>(`${environment.api}/BankAdmin/GetBankByID/${id}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  getBloodData(id:number): Observable<apiResponse<BloodGroupUpdateDto>> {
    return this.http.get<apiResponse<BloodGroupUpdateDto>>(`${environment.api}/BankAdmin/GetBloodData/${id}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  UpdateBloodData(body:BloodGroupUpdateDto): Observable<apiResponse<BloodGroupUpdateDto>> {
    return this.http.put<apiResponse<BloodGroupUpdateDto>>(`${environment.api}/BankAdmin/UpdateBloodData`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  updateBankImage(id: number, picture: File, removePicture: boolean): Observable<apiResponse<string>> {
    const dto = new FormData();
    dto.append('removePicture', removePicture.toString());
    dto.append('picture', picture);
    return this.http.put<apiResponse<string>>(`${environment.api}/BankAdmin/updatebankimage/${id}`, dto).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  addModerator(body:addModerator):Observable<apiResponse<string>>{
    return this.http.post<apiResponse<string>>(`${environment.api}/BankAdmin/addmoderator`,body).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
  deleteModerator(userId:number):Observable<apiResponse<string>>{
    return this.http.delete<apiResponse<string>>(`${environment.api}/BankAdmin/deletemoderator/${userId}`).pipe(
      catchError((error) => {
        console.error(error);
        throw error;
      })
    );
  }
}
