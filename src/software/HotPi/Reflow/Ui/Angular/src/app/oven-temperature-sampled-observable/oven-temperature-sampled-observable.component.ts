import { Component, OnInit, OnDestroy } from '@angular/core';

import { 
    HubService, 
    Hub, 
    HubSubscription, 
    HubWrapper 
} from 'ngx-signalr-hubservice';

import 'rxjs/add/operator/toPromise';

@Component({
  selector: 'hotpi-oven-temperature-sampled-observable',
  templateUrl: './oven-temperature-sampled-observable.component.html',
  styleUrls: ['./oven-temperature-sampled-observable.component.css']
})
@Hub({ hubName: 'ovenTemperatureSampledHub' })
export class OvenTemperatureSampledObservableComponent implements OnInit, OnDestroy {
  private connected: boolean;
  private hubWrapper: HubWrapper;

  constructor(private hubService: HubService) {
    this.hubWrapper = hubService.register(this);
	console.log('SERVICE REGISTERED...');
  }

  public async ngOnInit() {
	console.log('CONNECTING...');
    this.connected = await this.hubService.connect().toPromise();
	console.log('CONNECTED...' + this.connected);
  }

  public ngOnDestroy() {
    this.hubWrapper.unregister();
  }

  @HubSubscription()
  public observe(observed: any) {
    console.log(observed);
  }
}
