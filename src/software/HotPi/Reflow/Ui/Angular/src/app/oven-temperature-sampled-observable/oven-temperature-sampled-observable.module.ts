import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OvenTemperatureSampledObservableComponent } from './oven-temperature-sampled-observable.component';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    OvenTemperatureSampledObservableComponent
  ],
  declarations: [
    OvenTemperatureSampledObservableComponent
  ]
})
export class OvenTemperatureSampledObservableModule { }
