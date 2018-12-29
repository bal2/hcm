import { Injectable } from '@angular/core';
import { GroupModel } from './group.model';
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
}
