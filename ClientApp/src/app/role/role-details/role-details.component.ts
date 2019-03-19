import { Component, OnInit } from '@angular/core';
import { RoleModel, PermissionModel, RoleUserModel } from '../role.model';
import { RoleService } from '../role.service';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { AlertService } from 'src/app/alert.service';
import { MemberModel } from 'src/app/members/member.model';
import { ClrLoadingState } from '@clr/angular';

@Component({
  selector: 'app-role-details',
  templateUrl: './role-details.component.html',
  styleUrls: ['./role-details.component.scss']
})
export class RoleDetailsComponent implements OnInit {

  roleId: number;
  role: RoleModel;

  permissions: PermissionModel[];

  users: RoleUserModel[];
  addBtnStatus: ClrLoadingState = ClrLoadingState.DEFAULT;
  showUserSelect: boolean = false;

  constructor(
    private roleService: RoleService,
    private route: ActivatedRoute,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.roleId = Number.parseInt(this.route.snapshot.paramMap.get('id'));

    this.roleService.get(this.roleId)
      .subscribe((data) => {
        this.role = data;
      });

    forkJoin(this.roleService.getAllPermissions(), this.roleService.getRolePermissions(this.roleId))
      .subscribe((data) => {
        data[0].forEach(p => {
          p.inRole = data[1].find(x => x.permissionId == p.permissionId) != undefined;
        });

        this.permissions = data[0];
      });

    this.fetchUsers();
  }

  fetchUsers() {
    this.roleService.getRoleUsers(this.roleId)
      .subscribe((data) => {
        this.users = data;
      });
  }

  changePermission(p: PermissionModel) {
    if (p.inRole) {
      this.roleService.addRolePermission(this.roleId, p.permissionId)
        .subscribe(() => {
          this.alertService.addLocalAlert("success", "Permission: " + p.name + " successfully added!");
        }, () => {
          p.inRole = false;
        })
    }
    else {
      this.roleService.removeRolePermission(this.roleId, p.permissionId)
        .subscribe(() => {
          this.alertService.addLocalAlert("success", "Permission: " + p.name + " has been removed!");
        }, () => {
          p.inRole = true;
        })
    }
  }

  addUser(u: MemberModel) {
    this.showUserSelect = false;

    if (!u)
      return;

    this.addBtnStatus = ClrLoadingState.LOADING;

    this.roleService.addRoleUser(this.roleId, u.userId)
      .subscribe(() => {
        this.addBtnStatus = ClrLoadingState.SUCCESS;
        this.fetchUsers();
        this.alertService.addLocalAlert("success", "User added successfully");
      }, () => {
        this.addBtnStatus = ClrLoadingState.ERROR;
      });
  }

  removeUser(userId: number) {
    this.roleService.removeRoleUser(this.roleId, userId)
      .subscribe(() => {
        this.alertService.addLocalAlert("success", "User removed");
        this.fetchUsers();
      });
  }
}
