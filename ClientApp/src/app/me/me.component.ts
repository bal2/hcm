import { Component, OnInit, ViewChild } from '@angular/core';
import { MeModel } from './me.model';
import { MeService } from './me.service';
import { PictureUploadModalComponent } from '../picture-upload-modal/picture-upload-modal.component';

@Component({
  selector: 'app-me',
  templateUrl: './me.component.html',
  styleUrls: ['./me.component.scss']
})
export class MeComponent implements OnInit {

  me: MeModel;

  @ViewChild('picModal') picModal: PictureUploadModalComponent;

  constructor(private meService: MeService) { }

  ngOnInit() {
    this.fetchProfile();
  }

  fetchProfile() {
    this.me = null;
    
    this.meService.getMe()
      .subscribe((data) => {
        this.me = data;
      });
  }

}
