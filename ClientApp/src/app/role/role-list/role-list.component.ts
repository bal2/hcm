import { Component, OnInit } from '@angular/core';
import { RoleModel } from '../role.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ClrLoadingState, ClrDatagridStateInterface } from '@clr/angular';
import { RoleService } from '../role.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent implements OnInit {

  roles: RoleModel[];
  total: number;

  loading: boolean = true;

  showNewRoleForm: boolean = false;

  newRoleForm: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('')
  });

  postState: ClrLoadingState = ClrLoadingState.DEFAULT;

  constructor(
    private roleService: RoleService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  refresh(state: ClrDatagridStateInterface) {
    if (!state.page) //This is to prevent error when component is destroyed
      return;

    this.loading = true;

    this.roleService.getAll(Math.ceil((state.page.from / state.page.size) + 1), state.page.size).subscribe((data) => {
      this.roles = data.items;
      this.total = data.paging.totalItems;
      this.loading = false;
    });
  }

  openRole(id: number) {
    this.router.navigate(['system/roles/' + id]);
  }

  openCloseForm() {
    this.newRoleForm.reset();
    this.showNewRoleForm = !this.showNewRoleForm;
  }

  submitForm() {
    if (!this.newRoleForm.valid)
      return;

    this.newRoleForm.disable();
    this.postState = ClrLoadingState.LOADING;

    this.roleService.create(
      {
        name: this.newRoleForm.value.name,
        description: this.newRoleForm.value.description
      }
    )
      .subscribe((data) => {
        this.postState = ClrLoadingState.SUCCESS;
        this.newRoleForm.enable();
        this.openCloseForm();
        this.openRole(data.roleId);
      }, (error) => {
        this.newRoleForm.enable();
        this.postState = ClrLoadingState.ERROR;
        this.openCloseForm();
      })
  }

}
