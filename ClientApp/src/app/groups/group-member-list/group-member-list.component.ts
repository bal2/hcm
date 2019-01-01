import { Component, OnInit } from '@angular/core';
import { GroupService } from '../group.service';
import { GroupMemberModel } from '../group.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-group-member-list',
  templateUrl: './group-member-list.component.html',
  styleUrls: ['./group-member-list.component.scss']
})
export class GroupMemberListComponent implements OnInit {

  groupId: number;
  members: GroupMemberModel[];
  totalMembers: number;
  error: boolean;
  removalInProgress: boolean = false;

  constructor(
    private groupService: GroupService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.groupId = Number.parseInt(this.route.snapshot.paramMap.get('id'));

    this.fetchMembers();
  }

  fetchMembers() {
    this.members = null;

    this.groupService.getMembers(this.groupId, 1, 40)
      .subscribe((data) => {
        this.members = data.items;
        this.totalMembers = data.paging.totalItems;
      }, (err) => {
        this.error = true;
      });
  }

  removeMember(userId: number) {
    this.removalInProgress = true;

    this.groupService.removeMember(this.groupId, userId)
      .subscribe(() => {
        this.removalInProgress = false;
        this.fetchMembers();
      });
  }
}
