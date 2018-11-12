import { Component, OnInit } from '@angular/core';
import { MemberService } from '../members/member.service';
import { ActivatedRoute } from '@angular/router';
import { MemberDetailsModel } from '../members/memberDetails.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ClrLoadingState } from '@clr/angular';

@Component({
  selector: 'app-memberprofile',
  templateUrl: './memberprofile.component.html',
  styleUrls: ['./memberprofile.component.scss']
})
export class MemberprofileComponent implements OnInit {

  public member: MemberDetailsModel;

  private memberForm: FormGroup;

  private error: string;
  private loading: ClrLoadingState = ClrLoadingState.DEFAULT;

  constructor(
    private memberService: MemberService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.getMember();
  }

  getMember() {
    this.member = null;

    const id = this.route.snapshot.paramMap.get('id');

    this.memberService.get(Number.parseInt(id))
      .subscribe(member => {
        this.member = member

        this.memberForm = new FormGroup({
          firstName: new FormControl(member.firstName, [Validators.required]),
          lastName: new FormControl(member.lastName, [Validators.required]),
          email: new FormControl(member.email, [Validators.required, Validators.email]),
          title: new FormControl(member.title),
          phone: new FormControl(member.phone),
          cardId: new FormControl(member.cardId),
          isAdmin: new FormControl(member.isAdmin)
        });
      });
  }

  update() {
    const id = this.route.snapshot.paramMap.get('id');

    let m: MemberDetailsModel = {
      userId: Number.parseInt(id),
      email: this.memberForm.value.email,
      firstName: this.memberForm.value.firstName,
      lastName: this.memberForm.value.lastName,
      title: this.memberForm.value.title,
      phone: this.memberForm.value.phone,
      isPictureApproved: this.member.isPictureApproved,
      base64Picture: this.member.base64Picture,
      isAdmin: this.memberForm.value.isAdmin,
      cardId: this.memberForm.value.cardId,
      createdAt: this.member.createdAt,
      updatedAt: this.member.updatedAt
    }

    this.loading = ClrLoadingState.LOADING;

    this.memberService.update(m.userId, m)
      .subscribe((data) => {
        this.loading = ClrLoadingState.SUCCESS;
        this.member = data;
      }, (error) => {
        this.error = error;
        this.loading = ClrLoadingState.ERROR;
      })
  }

  approvePicture() {
    let m = Object.assign({}, this.member);

    m.isPictureApproved = true;

    this.loading = ClrLoadingState.LOADING;

    this.memberService.update(this.member.userId, m)
      .subscribe((data) => {
        this.loading = ClrLoadingState.SUCCESS;
        this.member = data;
      }, (error) => {
        this.error = error;
        this.loading = ClrLoadingState.ERROR;
      })
  }

}
