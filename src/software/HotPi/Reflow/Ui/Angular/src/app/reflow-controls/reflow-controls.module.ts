import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReflowControlsService } from './reflow-controls.service';
import { ReflowControlsComponent } from './reflow-controls.component';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    ReflowControlsComponent
  ],
  declarations: [
    ReflowControlsComponent
  ],
  providers: [
    ReflowControlsService
  ]
})
export class ReflowControlsModule { }
