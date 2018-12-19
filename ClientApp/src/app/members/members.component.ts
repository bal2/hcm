import { Component, OnInit } from '@angular/core';
import { MemberModel, NewMemberModel } from './member.model';
import { MemberService } from './member.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss']
})
export class MembersComponent implements OnInit {

  members: MemberModel[];

  showNewUserForm: boolean = false;

  newUserForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    title: new FormControl(''),
    phone: new FormControl('')
  });

  error: string;

  constructor(private memberService: MemberService, private router: Router) { }

  ngOnInit() {
    this.getMembers();
  }

  getMembers() {
    this.memberService.getAll().subscribe((data) => {
      this.members = data.items;
      console.log(data);
    });
  }

  openUser(id: number) {
    this.router.navigate(['members/' + id]);
  }

  openCloseForm() {
    this.showNewUserForm = !this.showNewUserForm;
    this.newUserForm.reset();
  }

  submitForm() {
    if (!this.newUserForm.valid)
      return;

    this.newUserForm.disable();

    this.memberService.create(
      {
        email: this.newUserForm.value.email,
        password: this.newUserForm.value.password,
        firstName: this.newUserForm.value.firstName,
        lastName: this.newUserForm.value.lastName,
        title: this.newUserForm.value.title,
        phone: this.newUserForm.value.phone
      }
    )
      .subscribe((data) => {
        this.error = null;
        this.newUserForm.enable();
        this.openCloseForm();
        this.getMembers();
      }, (error) => {
        this.error = error;
        this.newUserForm.enable();
      });
  }

}
