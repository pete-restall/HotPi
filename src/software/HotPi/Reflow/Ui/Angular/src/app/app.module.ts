import { BrowserModule } from '@angular/platform-browser';
import { NgModule, InjectionToken } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { NbThemeModule } from '@nebular/theme';
import { HubService } from 'ngx-signalr-hubservice';

import { AppComponent } from './app.component';
import { IHaveStartReflowLink, I_HAVE_START_REFLOW_LINK } from './reflow-controls/i-have-start-reflow-link.interface';
import { PageContainerModule } from './page-container/page-container.module';
import { RootViewModelService } from './root-view-model.service';

const appRoutes: Routes = [];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    NbThemeModule.forRoot({ name: 'default' }),
	PageContainerModule
  ],
  providers: [
    HubService,
    { provide: I_HAVE_START_REFLOW_LINK, useClass: RootViewModelService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
