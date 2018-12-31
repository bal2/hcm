import { Injectable } from '@angular/core';
import { GroupModel, NewGroupModel } from './group.model';
import { ListResponse } from '../listResponse.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

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
}
