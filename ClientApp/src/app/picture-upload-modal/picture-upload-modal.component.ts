import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { MeService } from '../me/me.service';
import { UserPictureModel } from './userPicture.model';
import { ClrLoadingState } from '@clr/angular';
import { MemberService } from '../members/member.service';

@Component({
  selector: 'app-picture-upload-modal',
  templateUrl: './picture-upload-modal.component.html',
  styleUrls: ['./picture-upload-modal.component.scss']
})
export class PictureUploadModalComponent implements OnInit {

  isOpen: boolean = false;

  userId: number;

  public base64Img: string;
  public fileName: string;
  public imageChangedEvent: any = '';
  public error: string;
  public loading = ClrLoadingState.DEFAULT;

  public file;

  @Output() pictureUploaded: EventEmitter<string> = new EventEmitter();

  constructor(private meService: MeService, private memberService: MemberService) { }

  ngOnInit() {
  }

  open(userId: number = null) {
    this.userId = userId;

    this.isOpen = true;
  }

  close() {
    this.isOpen = false;
    this.base64Img = null;
    this.fileName = null;
    this.imageChangedEvent = null;
    this.error = null;
    this.loading = ClrLoadingState.DEFAULT;
  }

  public onFileChanged(event: any) {
    this.imageChangedEvent = event;
    try {
      this.fileName = event.target.files[0].name;
    } catch (e) {
      this.base64Img = null;
      this.fileName = null;
    }
  }

  public imageCropped(image: string) {
    this.base64Img = image.split(',')[1]; //remove metadata
  }

  public imageLoaded() {
    this.error = null;
  }

  public loadImageFailed() {
    this.error = "Failed to load image. Are you sure you selected an image file?"
    this.fileName = null;
    this.base64Img = null;
  }

  public uploadPicture() {
    let obj: UserPictureModel = {
      base64Picture: this.base64Img,
      approved: false
    }

    this.loading = ClrLoadingState.LOADING;

    if (!this.userId) {
      this.meService.uploadPicture(obj)
        .subscribe((data) => {
          this.loading = ClrLoadingState.SUCCESS;
          this.pictureUploaded.emit(data.base64Picture);
          this.close();
        }, (err) => {
          this.error = err;
          this.loading = ClrLoadingState.ERROR;
        });
    }
    else {
      this.memberService.uploadPicture(this.userId, obj)
        .subscribe((data) => {
          this.loading = ClrLoadingState.SUCCESS;
          this.pictureUploaded.emit(data.base64Picture);
          this.close();
        }, (err) => {
          this.error = err;
          this.loading = ClrLoadingState.ERROR;
        });
    }
  }

}
