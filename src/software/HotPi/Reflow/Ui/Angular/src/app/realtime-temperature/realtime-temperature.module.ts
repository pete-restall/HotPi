import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgxEchartsModule } from 'ngx-echarts';

import { OvenTemperatureSampledObservableModule } from '../oven-temperature-sampled-observable/oven-temperature-sampled-observable.module';
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
  ]
})
export class RealtimeTemperatureModule { }
