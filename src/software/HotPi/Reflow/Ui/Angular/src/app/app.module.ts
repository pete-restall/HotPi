import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NbThemeModule } from '@nebular/theme';
import { HubService } from 'ngx-signalr-hubservice';

import { AppComponent } from './app.component';
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
    NbThemeModule.forRoot({ name: 'default' }),
	PageContainerModule
  ],
  providers: [
    HubService,
    RootViewModelService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
