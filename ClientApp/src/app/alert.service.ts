import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router, RouterEvent, NavigationStart } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  private _globalAlerts: BehaviorSubject<GlobalAlertModel[]> = new BehaviorSubject([]);
  public readonly globalAlerts: Observable<GlobalAlertModel[]> = this._globalAlerts.asObservable();

  private _localAlerts: BehaviorSubject<LocalAlertModel[]> = new BehaviorSubject([]);
  public readonly localAlerts: Observable<LocalAlertModel[]> = this._localAlerts.asObservable();

  constructor(private router: Router) {
    //Clear local alerts when route is changed
    router.events.subscribe((event: RouterEvent) => {
      if (event instanceof NavigationStart)
        this.clearLocalAlerts();
    })
  }

  addGlobalAlert(type: string, message: string) {
    let alerts: GlobalAlertModel[] = this._globalAlerts.getValue();

    let a: GlobalAlertModel = {
      id: new Date().getUTCMilliseconds(),
      type: type,
      message: message
    };

    alerts.push(a);

    this._globalAlerts.next(alerts);
  }

  closeGlobalAlert(id: number) {
    this._globalAlerts.next(this._globalAlerts.getValue().filter(x => x.id !== id));
  }

  getGlobalAlert(id: number): GlobalAlertModel {
    return this._globalAlerts.getValue().find(a => a.id == id);
  }

  addLocalAlert(type: string, message: string) {
    let alerts: LocalAlertModel[] = this._localAlerts.getValue();

    let a: LocalAlertModel = {
      id: new Date().getUTCMilliseconds(),
      type: type,
      message: message
    };

    alerts.push(a);

    if(alerts.length > 3)
      alerts.shift();

    this._localAlerts.next(alerts);
  }

  closeLocalAlert(id: number) {
    this._localAlerts.next(this._localAlerts.getValue().filter(x => x.id !== id));
  }

  clearLocalAlerts() {
    this._localAlerts.next([]);
  }
}

export class GlobalAlertModel {
  id: number;
  type: string;
  message: string;
}

export class LocalAlertModel {
  id: number;
  type: string;
  message: string;
}