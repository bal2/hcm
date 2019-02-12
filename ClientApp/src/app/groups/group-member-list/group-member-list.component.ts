import { Component, OnInit } from '@angular/core';
import { GroupService } from '../group.service';
import { GroupMemberModel, GroupMemberDetailsModel } from '../group.model';
import { ActivatedRoute } from '@angular/router';
import { ClrLoadingState } from '@clr/angular';
import { MemberModel } from 'src/app/members/member.model';
import { AlertService } from 'src/app/alert.service';

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

  removalBtnStatus: ClrLoadingState = ClrLoadingState.DEFAULT;
  adminBtnStatus: ClrLoadingState = ClrLoadingState.DEFAULT;
  memberPopup: boolean = false;
  member: GroupMemberDetailsModel;

  addBtnStatus: ClrLoadingState = ClrLoadingState.DEFAULT;
  showUserSelect: boolean = false;

  constructor(
    private groupService: GroupService,
    private route: ActivatedRoute,
    private alertService: AlertService
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

  fetchMember(userId: number) {
    this.groupService.getMemberDetails(this.groupId, userId)
      .subscribe((m) => {
        this.member = m;
        this.memberPopup = true;
      });
  }

  removeMember(userId: number) {
    this.removalBtnStatus = ClrLoadingState.LOADING;

    this.groupService.removeMember(this.groupId, userId)
      .subscribe(() => {
        this.removalBtnStatus = ClrLoadingState.SUCCESS;
        this.fetchMembers();
        this.memberPopup = false;
        this.alertService.addLocalAlert("info", "User has been removed");
      }, () => {
        this.removalBtnStatus = ClrLoadingState.ERROR;
      });
  }

  setAdmin(userId: number, value: boolean) {
    this.adminBtnStatus = ClrLoadingState.LOADING;

    this.member.isGroupAdmin = value;

    this.groupService.updateMember(this.groupId, userId, this.member)
      .subscribe(() => {
        this.adminBtnStatus = ClrLoadingState.SUCCESS;
      }, () => {
        this.adminBtnStatus = ClrLoadingState.ERROR;
      });
  }

  addMember(u: MemberModel) {
    this.showUserSelect = false;

    if (!u)
      return;

    this.addBtnStatus = ClrLoadingState.LOADING;

    this.groupService.addMember(this.groupId, u.userId)
      .subscribe(() => {
        this.addBtnStatus = ClrLoadingState.SUCCESS;
        this.fetchMembers();
        this.alertService.addLocalAlert("success", "User added successfully");
      }, () => {
        this.addBtnStatus = ClrLoadingState.ERROR;
      });
  }
}
