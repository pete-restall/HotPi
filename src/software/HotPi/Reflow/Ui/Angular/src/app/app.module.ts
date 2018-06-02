import { BrowserModule } from '@angular/platform-browser';
import { NgModule, InjectionToken } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { NbThemeModule } from '@nebular/theme';
import { HubService } from 'ngx-signalr-hubservice';

import { AppComponent } from './app.component';
import { BugfixInterceptorForNoDeserialisedErrorResponse } from './bugfix-interceptor-for-no-deserialised-error-response.class';
import { I_HAVE_SUMMARY_OF_ALL_PROFILES_LINK } from './reflow-controls/i-have-summary-of-all-profiles-link.interface';
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
    { provide: I_HAVE_SUMMARY_OF_ALL_PROFILES_LINK, useClass: RootViewModelService },
    { provide: HTTP_INTERCEPTORS, useClass: BugfixInterceptorForNoDeserialisedErrorResponse, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
