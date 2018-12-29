import { Component, OnInit } from '@angular/core';
import { GroupModel } from './group.model';
import { ClrDatagridStateInterface, ClrLoadingState } from '@clr/angular';
import { GroupService } from './group.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  groups: GroupModel[];
  total: number;

  loading: boolean = true;

  showNewGroupForm: boolean = false;

  newGroupForm: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    shortName: new FormControl('', [Validators.required]),
    description: new FormControl('')
  });

  error: string;
  postState: ClrLoadingState = ClrLoadingState.DEFAULT;

  constructor(private groupService: GroupService) { }

  ngOnInit() {
  }

  //Called to populate datagrid
  refresh(state: ClrDatagridStateInterface) {
    if (!state.page) //This happens since refresh is called when component is destroyed
      return;

    this.loading = true;

    this.groupService.getAll(Math.ceil((state.page.from / state.page.size) + 1), state.page.size).subscribe((data) => {
      this.groups = data.items;
      this.total = data.paging.totalItems;
      this.loading = false;
    });
  }

  //Open and close form modal
  openCloseForm() {
    this.newGroupForm.reset();
    this.showNewGroupForm = !this.showNewGroupForm;
  }

  submitForm() {
    if (!this.newGroupForm.valid)
      return;

    this.newGroupForm.disable();
    this.postState = ClrLoadingState.LOADING;

    this.groupService.create(
      {
        name: this.newGroupForm.value.name,
        shortName: this.newGroupForm.value.shortName,
        description: this.newGroupForm.value.description
      }
    )
      .subscribe((data) => {
        this.error = null;
        this.postState = ClrLoadingState.SUCCESS;
        this.newGroupForm.enable();
        this.openCloseForm();
      }, (error) => {
        this.error = error;
        this.newGroupForm.enable();
        this.postState = ClrLoadingState.ERROR;
      })
  }

}
