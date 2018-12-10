import { Component } from '@angular/core';
import { AuthenticationService } from './login/authentication.service';
import { GlobalAlertService } from './global-alert.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  collapsed: boolean = true;

  constructor(public authService: AuthenticationService, public alertService: GlobalAlertService) { }
}
