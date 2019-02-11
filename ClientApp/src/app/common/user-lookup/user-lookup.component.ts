import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MemberModel } from 'src/app/members/member.model';
import { MemberService } from 'src/app/members/member.service';
import { ClrDatagridStateInterface } from '@clr/angular';

@Component({
  selector: 'app-user-lookup',
  templateUrl: './user-lookup.component.html',
  styleUrls: ['./user-lookup.component.scss']
})
export class UserLookupComponent implements OnInit {
  @Input() showComponent: boolean;
  @Output() userSelected: EventEmitter<MemberModel> = new EventEmitter();

  members: MemberModel[];
  total: number;
  loading: boolean = true;

  constructor(private memberService: MemberService) { }

  ngOnInit() {
  }

  refresh(state: ClrDatagridStateInterface) {
    if (!state.page) //This happens since refresh is called when component is destroyed
      return;

    this.loading = true;

    this.memberService.getAll(Math.ceil((state.page.from / state.page.size) + 1), state.page.size).subscribe((data) => {
      this.members = data.items;
      this.total = data.paging.totalItems;
      this.loading = false;
    });
  }

  selectUser(m: MemberModel) {
    this.userSelected.emit(m);
  }

  test() {
    alert("test");
  }

}
