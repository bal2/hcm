import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { MeComponent } from './me/me.component';
import { MembersComponent } from './members/members.component';
import { PictureUploadModalComponent } from './picture-upload-modal/picture-upload-modal.component';
import { MemberprofileComponent } from './memberprofile/memberprofile.component';
import { CardaccessComponent } from './cardaccess/cardaccess.component';
import { AppRoutingModule } from './app-routing.module';
import { ClarityModule } from '@clr/angular';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ImageCropperModule } from 'ngx-image-cropper';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { GroupsComponent } from './groups/groups.component';
import { GroupDetailsComponent } from './groups/group-details/group-details.component';
import { GroupMemberListComponent } from './groups/group-member-list/group-member-list.component';
import { UserLookupComponent } from './common/user-lookup/user-lookup.component';
import { RoleListComponent } from './role/role-list/role-list.component';
import { RoleDetailsComponent } from './role/role-details/role-details.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MeComponent,
    MembersComponent,
    PictureUploadModalComponent,
    MemberprofileComponent,
    CardaccessComponent,
    GroupsComponent,
    GroupDetailsComponent,
    GroupMemberListComponent,
    UserLookupComponent,
    RoleListComponent,
    RoleDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ClarityModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    ImageCropperModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }