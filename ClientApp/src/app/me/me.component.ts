import { Component, OnInit } from '@angular/core';
import { MeModel } from './me.model';
import { MeService } from './me.service';

@Component({
  selector: 'app-me',
  templateUrl: './me.component.html',
  styleUrls: ['./me.component.scss']
})
export class MeComponent implements OnInit {

  me: MeModel;

  constructor(private meService: MeService) { }

  ngOnInit() {
    this.meService.getMe()
      .subscribe((data) => {
        this.me = data;
      });
  }

}
