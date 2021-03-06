import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MemberModel, NewMemberModel } from './member.model';
import { MemberDetailsModel } from './memberDetails.model';
import { UserPictureModel } from '../picture-upload-modal/userPicture.model';
import { ListResponse } from '../listResponse.model';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private http: HttpClient) { }

  getAll(pageNumber = 1, pageSize = 10): Observable<ListResponse<MemberModel>> {
    return this.http.get<ListResponse<MemberModel>>("/api/users?PageNumber=" + pageNumber + "&PageSize=" + pageSize);
  }

  create(m: NewMemberModel): Observable<NewMemberModel> {
    return this.http.post<NewMemberModel>("/api/users", m);
  }

  get(id: number): Observable<MemberDetailsModel> {
    return this.http.get<MemberDetailsModel>("/api/users/" + id);
  }

  update(id: number, m: MemberDetailsModel): Observable<MemberDetailsModel> {
    return this.http.put<MemberDetailsModel>("/api/users/" + id, m);
  }

  uploadPicture(id: number, pic: UserPictureModel): Observable<UserPictureModel> {
    return this.http.post<UserPictureModel>(`/api/users/${id}/picture`, pic);
  }
}