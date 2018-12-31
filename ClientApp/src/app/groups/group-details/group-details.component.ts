import { Component, OnInit } from '@angular/core';
import { GroupModel } from '../group.model';
import { GroupService } from '../group.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.scss']
})
export class GroupDetailsComponent implements OnInit {

  groupId: number;
  group: GroupModel;

  constructor(
    private groupService: GroupService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.groupId = Number.parseInt(this.route.snapshot.paramMap.get('id'));

    this.groupService.get(this.groupId)
      .subscribe((data) => {
        this.group = data;
      });
  }

}
