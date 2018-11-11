import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MeModel } from './me.model';
import { UserPictureModel } from '../picture-upload-modal/userPicture.model';

@Injectable({
  providedIn: 'root'
})
export class MeService {

  constructor(private http: HttpClient) { }

  getMe(): Observable<MeModel> {
    return this.http.get<MeModel>("/api/me");
  }

  uploadPicture(pic: UserPictureModel): Observable<UserPictureModel> {
    return this.http.post<UserPictureModel>("/api/me/picture", pic);
  }
}
