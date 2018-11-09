import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MemberModel, NewMemberModel } from './member.model';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<MemberModel[]> {
    return this.http.get<MemberModel[]>("/api/users");
  }

  create(m: NewMemberModel): Observable<NewMemberModel> {
    return this.http.post<NewMemberModel>("/api/users", m);
  }
}