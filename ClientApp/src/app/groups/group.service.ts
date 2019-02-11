import { Injectable } from '@angular/core';
import { GroupModel, NewGroupModel, GroupMemberModel, GroupMemberDetailsModel } from './group.model';
import { ListResponse } from '../listResponse.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { MemberDetailsModel } from '../members/memberDetails.model';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private http: HttpClient) { }

  getAll(pageNumber = 1, pageSize = 10): Observable<ListResponse<GroupModel>> {
    return this.http.get<ListResponse<GroupModel>>("/api/groups?PageNumber=" + pageNumber + "&PageSize=" + pageSize);
  }

  create(g: NewGroupModel): Observable<GroupModel> {
    return this.http.post<GroupModel>("/api/groups", g);
  }

  get(id: number): Observable<GroupModel> {
    return this.http.get<GroupModel>("/api/groups/" + id);
  }

  getMembers(id: number, pageNumber = 1, pageSize = 10): Observable<ListResponse<GroupMemberModel>> {
    return this.http.get<ListResponse<GroupMemberModel>>("/api/groups/" + id + "/members?PageNumber=" + pageNumber + "&PageSize=" + pageSize);
  }

  addMember(id: number, userId: number): Observable<GroupMemberDetailsModel> {
    let obj = {
      userId: userId
    }

    return this.http.post<GroupMemberDetailsModel>("/api/groups/" + id + "/members", obj);
  }

  getMemberDetails(id: number, userId: number): Observable<GroupMemberDetailsModel> {
    return this.http.get<GroupMemberDetailsModel>("/api/groups/" + id + "/members/" + userId);
  }

  removeMember(id: number, userId: number): Observable<object> {
    return this.http.delete("/api/groups/" + id + "/members/" + userId);
  }

  updateMember(id: number, userId: number, m: GroupMemberDetailsModel): Observable<GroupMemberDetailsModel> {
    return this.http.put<GroupMemberDetailsModel>("/api/groups/" + id + "/members/" + userId, m);
  }
}
