import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MemberModel } from '../members/member.model';

@Component({
  selector: 'app-cardaccess',
  templateUrl: './cardaccess.component.html',
  styleUrls: ['./cardaccess.component.scss']
})
export class CardaccessComponent implements OnInit {

  public cardId: string;
  public success: MemberModel;
  public error: string;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  scanCard() {
    if (!this.cardId)
      return;

    let obj = { cardId: this.cardId };

    this.http.post<MemberModel>("/api/access", obj)
      .subscribe((data) => {
        this.cardId = null;
        this.flashGreen(data);
      }, (error) => {
        this.cardId = null;
        this.flashRed(error);
      });
  }

  toNormal() {
    this.error = null;
    this.success = null;
  }

  flashGreen(m: MemberModel) {
    this.success = m;
    setTimeout(() => this.toNormal(), 1000)
  }

  flashRed(e: string) {
    this.error = e;
    setTimeout(() => this.toNormal(), 1000)
  }

}
