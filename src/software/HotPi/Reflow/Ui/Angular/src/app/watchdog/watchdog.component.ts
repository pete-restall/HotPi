import { Component, OnInit, OnDestroy } from '@angular/core';

import { 
    HubService, 
    Hub, 
    HubSubscription, 
    HubWrapper 
} from 'ngx-signalr-hubservice';

import 'rxjs/add/operator/toPromise';

@Component({
  selector: 'hotpi-watchdog',
  templateUrl: './watchdog.component.html',
  styleUrls: ['./watchdog.component.css']
})
@Hub({ hubName: 'ovenTemperatureSampledHub' })
export class WatchdogComponent implements OnInit, OnDestroy {
  private connected: boolean;
  private hub: HubWrapper;

  private _lastEventObservedAt: Date = null;
  private _lastEventHadTimerExpired: boolean;

  constructor(private hubService: HubService) {
    this.hub = hubService.register(this);
  }

  public async ngOnInit() {
    this.connected = await this.hubService.connect().toPromise();
  }

  public ngOnDestroy() {
    this.hub.unregister();
  }

  @HubSubscription()
  public observe(observed: any) {
    this._lastEventObservedAt = new Date(observed.Timestamp);
    this._lastEventHadTimerExpired = observed.TimerExpired;
  }

  public get lastEventObservedAt(): Date {
    return this._lastEventObservedAt;
  }

  public get lastEventHadTimerExpired(): boolean {
    return this._lastEventHadTimerExpired;
  }
}
