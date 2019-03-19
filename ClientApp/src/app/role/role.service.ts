import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListResponse } from '../listResponse.model';
import { RoleModel, NewRoleModel, RoleUserModel, PermissionModel } from './role.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http: HttpClient) { }

  public getAll(pageNumber = 1, pageSize = 10): Observable<ListResponse<RoleModel>> {
    return this.http.get<ListResponse<RoleModel>>("/api/roles?PageNumber=" + pageNumber + "&PageSize=" + pageSize);
  }

  public create(r: NewRoleModel): Observable<RoleModel> {
    return this.http.post<RoleModel>("/api/roles", r);
  }

  public get(id: number): Observable<RoleModel> {
    return this.http.get<RoleModel>("/api/roles/" + id);
  }

  public update(id: number, r: RoleModel) {
    return this.http.put<RoleModel>("/api/roles/" + id, r);
  }

  public getRoleUsers(id: number): Observable<RoleUserModel[]> {
    return this.http.get<RoleUserModel[]>("/api/roles/" + id + "/users");
  }

  public addRoleUser(id: number, userId: number): Observable<void> {
    return this.http.post<void>("/api/roles/" + id + "/users", { userId: userId });
  }

  public removeRoleUser(id: number, userId: number): Observable<void> {
    return this.http.delete<void>("/api/roles/" + id + "/users/" + userId);
  }

  public getRolePermissions(id: number): Observable<PermissionModel[]> {
    return this.http.get<PermissionModel[]>("/api/roles/" + id + "/permissions");
  }

  public addRolePermission(id: number, permissionId: number): Observable<void> {
    return this.http.post<void>("/api/roles/" + id + "/permissions", { permissionId: permissionId });
  }

  public removeRolePermission(id: number, permissionId: number): Observable<void> {
    return this.http.delete<void>("/api/roles/" + id + "/permissions/" + permissionId);
  }

  public getAllPermissions(): Observable<PermissionModel[]> {
    return this.http.get<PermissionModel[]>("/api/roles/permissions");
  }
}
