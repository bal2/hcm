import { Component } from '@angular/core';
import { AuthenticationService } from './login/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  collapsed: boolean = true;

  constructor(private authService: AuthenticationService) { }
}
