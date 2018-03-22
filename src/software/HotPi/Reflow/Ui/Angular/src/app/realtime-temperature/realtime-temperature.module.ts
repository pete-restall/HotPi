import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgxEchartsModule } from 'ngx-echarts';
import { Observable } from 'rxjs/Observable';

import { OvenTemperatureSampledObservableModule } from '../oven-temperature-sampled-observable/oven-temperature-sampled-observable.module';
import { OvenTemperatureSampledObservableComponent } from '../oven-temperature-sampled-observable/oven-temperature-sampled-observable.component';
import { OvenTemperatureSampled } from '../oven-temperature-sampled-observable/oven-temperature-sampled';
import { RealtimeTemperatureComponent } from './realtime-temperature.component';

@NgModule({
  imports: [
    CommonModule,
    NgxEchartsModule,
    OvenTemperatureSampledObservableModule
  ],
  exports: [
    RealtimeTemperatureComponent
  ],
  declarations: [
    RealtimeTemperatureComponent
  ],
  providers: [
    OvenTemperatureSampledObservableComponent,
    {
      provide: Observable,
      useFactory: (ob: OvenTemperatureSampledObservableComponent) => { return ob.ovenTemperatureSampled; },
      deps: [OvenTemperatureSampledObservableComponent]
    }
  ]
})
export class RealtimeTemperatureModule { }
