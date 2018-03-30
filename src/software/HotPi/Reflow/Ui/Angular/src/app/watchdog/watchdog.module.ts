import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WatchdogComponent } from './watchdog.component';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    WatchdogComponent
  ],
  declarations: [
    WatchdogComponent
  ]
})
export class WatchdogModule { }
