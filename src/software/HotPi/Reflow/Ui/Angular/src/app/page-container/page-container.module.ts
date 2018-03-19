import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NbSidebarModule, NbLayoutModule, NbSidebarService } from '@nebular/theme';

import { RealtimeTemperatureModule } from '../realtime-temperature/realtime-temperature.module';
import { PageContainerComponent } from './page-container.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NbLayoutModule,
    NbSidebarModule,
    RealtimeTemperatureModule
  ],
  exports: [PageContainerComponent],
  providers: [NbSidebarService],
  declarations: [PageContainerComponent]
})
export class PageContainerModule { }
