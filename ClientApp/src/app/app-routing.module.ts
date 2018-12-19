import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MeComponent } from './me/me.component';
import { AuthGuard } from './login/auth.guard';
import { LoginComponent } from './login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MembersComponent } from './members/members.component';
import { MemberprofileComponent } from './memberprofile/memberprofile.component';
import { CardaccessComponent } from './cardaccess/cardaccess.component';

const routes: Routes = [
  { path: "", redirectTo: "me", pathMatch: "full" },
  { path: "me", component: MeComponent, canActivate: [AuthGuard] },
  { path: "members", component: MembersComponent, canActivate: [AuthGuard] },
  { path: "members/:id", component: MemberprofileComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'access', component: CardaccessComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    HttpClientModule
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
