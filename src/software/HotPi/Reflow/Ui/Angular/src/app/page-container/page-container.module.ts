import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NbSidebarModule, NbLayoutModule, NbSidebarService } from '@nebular/theme';

import { PageContainerComponent } from './page-container.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NbLayoutModule,
    NbSidebarModule
  ],
  exports: [PageContainerComponent],
  providers: [NbSidebarService],
  declarations: [PageContainerComponent]
})
export class PageContainerModule { }
