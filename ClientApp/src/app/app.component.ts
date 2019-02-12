import { Component } from '@angular/core';
import { AuthenticationService } from './login/authentication.service';
import { AlertService } from './alert.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  
  collapsed: boolean = true;

  constructor(public authService: AuthenticationService, public alertService: AlertService) { }
}
