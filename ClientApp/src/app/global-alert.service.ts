import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GlobalAlertService {

  private _alerts: BehaviorSubject<GlobalAlertModel[]> = new BehaviorSubject([]);
  public readonly alerts: Observable<GlobalAlertModel[]> = this._alerts.asObservable();

  constructor() { }

  addAlert(type: string, message: string) {
    let alerts: GlobalAlertModel[] = this._alerts.getValue();

    let a: GlobalAlertModel = {
      id: alert.length,
      type: type,
      message: message
    };

    alerts.push(a);

    this._alerts.next(alerts);
  }

  close(id: number) {
    this._alerts.next(this._alerts.getValue().filter(x => x.id !== id));
  }

  getAlert(id: number): GlobalAlertModel {
    return this._alerts.getValue().find(a => a.id == id);
  }
}

export class GlobalAlertModel {
  id: number;
  type: string;
  message: string;
}