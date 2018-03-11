import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgxEchartsModule } from 'ngx-echarts';

import { RealtimeTemperatureComponent } from './realtime-temperature.component';

@NgModule({
  imports: [
    CommonModule,
    NgxEchartsModule
  ],
  exports: [
    RealtimeTemperatureComponent
  ],
  declarations: [RealtimeTemperatureComponent]
})
export class RealtimeTemperatureModule { }
