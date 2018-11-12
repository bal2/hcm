import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { ClrLoadingState } from '@clr/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  public loginBtnState: ClrLoadingState = ClrLoadingState.DEFAULT;
  private returnUrl: string;
  public loginError: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login() {
    if (!this.loginForm.valid)
      return;

    this.loginForm.disable();
    this.loginBtnState = ClrLoadingState.LOADING;

    this.authenticationService.login(this.loginForm.value.email, this.loginForm.value.password)
      .subscribe(
        (data) => {
          this.loginBtnState = ClrLoadingState.SUCCESS;
          this.router.navigate([this.returnUrl]);
        },
        (error) => {
          this.loginError = error;
          this.loginForm.enable();
          this.loginBtnState = ClrLoadingState.ERROR;
        })
  }

}
