import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { NgSelectModule } from '@ng-select/ng-select';

import { ReflowControlsService } from './reflow-controls.service';
import { ReflowControlsComponent } from './reflow-controls.component';

@NgModule({
  imports: [
    CommonModule,
    NgSelectModule,
	FormsModule
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
