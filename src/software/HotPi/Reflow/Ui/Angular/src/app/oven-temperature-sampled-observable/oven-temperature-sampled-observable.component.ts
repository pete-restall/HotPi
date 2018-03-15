import { Component, OnInit, OnDestroy } from '@angular/core';

import { 
    HubService, 
    Hub, 
    HubSubscription, 
    HubWrapper 
} from 'ngx-signalr-hubservice';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';

import { OvenTemperatureSampled } from './oven-temperature-sampled';
import { Temperature } from './temperature';

@Component({
  selector: 'hotpi-oven-temperature-sampled-observable',
  templateUrl: './oven-temperature-sampled-observable.component.html',
  styleUrls: ['./oven-temperature-sampled-observable.component.css']
})
@Hub({ hubName: 'ovenTemperatureSampledHub' })
export class OvenTemperatureSampledObservableComponent implements OnInit, OnDestroy {
  private connected: boolean;
  private hubWrapper: HubWrapper;
  private observable: Observable<OvenTemperatureSampled>;
  private subscribers: any;

  constructor(private hubService: HubService) {
    this.hubWrapper = hubService.register(this);

	let outer = this;
    this.observable = new Observable<OvenTemperatureSampled>(subscribers => {
      outer.subscribers = subscribers;
    });
  }

  public async ngOnInit() {
    this.connected = await this.hubService.connect().toPromise();
  }

  public ngOnDestroy() {
    this.hubWrapper.unregister();
  }

  @HubSubscription()
  public observe(observed: any) {
    if (typeof(this.subscribers) === 'undefined')
      return;

    this.subscribers.next(new OvenTemperatureSampled(new Date(observed.Timestamp), new Temperature(observed.Temperature.Kelvin)));
  }

  public get ovenTemperatureSampled(): Observable<OvenTemperatureSampled> {
    return this.observable;
  }
}
