import { Component, OnInit } from '@angular/core';
import { GroupModel } from './group.model';
import { ClrDatagridStateInterface } from '@clr/angular';
import { GroupService } from './group.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  groups: GroupModel[];
  total: number;

  loading: boolean = true;

  constructor(private groupService: GroupService) { }

  ngOnInit() {
  }

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

}
