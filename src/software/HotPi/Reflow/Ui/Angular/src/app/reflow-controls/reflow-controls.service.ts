import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

import { retry, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/throw';

import { IHaveSummaryOfAllProfilesLink, I_HAVE_SUMMARY_OF_ALL_PROFILES_LINK } from './i-have-summary-of-all-profiles-link.interface';
import { SummaryOfAllProfiles } from './summary-of-all-profiles.interface';
import { ProfileSummary } from './profile-summary.interface';
import { CurrentReflow } from './current-reflow.interface';

const useJson = new HttpHeaders({'Accept': 'application/json', 'Content-Type': 'application/json'});

@Injectable()
export class ReflowControlsService {
  constructor(
    private httpClient: HttpClient,
    @Inject(I_HAVE_SUMMARY_OF_ALL_PROFILES_LINK) private links: IHaveSummaryOfAllProfilesLink) {
  }

  public getSummaryOfAllProfiles(): Observable<SummaryOfAllProfiles> {
    return this
      .httpClient
      .get<SummaryOfAllProfiles>(this.links.summaryOfAllProfilesLink)
      .pipe(
        retry(3),
        catchError(error => this.errorDebug(error, <SummaryOfAllProfiles> { profiles: [] })));
  }

  private errorDebug<T>(details: HttpErrorResponse, observableValue: T): Observable<T> {
    console.error(details);
    return Observable.of(observableValue);
  }

  public startReflow(profile: ProfileSummary): Observable<CurrentReflow> {
    if (!profile || !profile.id)
      return Observable.throw(new Error("Parameter 'profile' is not in a valid state"));

  return this
      .httpClient
      .request<CurrentReflow>('START', profile.startReflowLink, {
        body: { },
        headers: useJson
      })
      .pipe(catchError(error =>
        error.status == 409
          ? Observable.of<CurrentReflow>(error.error)
          : this.errorDebug(error, <CurrentReflow> { profileId: '', processRunId: '', abortLink: '' })));
  }

  public abortReflow(currentReflow: CurrentReflow): Observable<Object> {
    if (!currentReflow || !currentReflow.processRunId)
      return Observable.throw(new Error("Parameter 'currentReflow' is not in a valid state"));

    return this
      .httpClient
      .request('ABORT', currentReflow.abortLink, {
        body: { },
        headers: useJson
      })
      .pipe(catchError(error => this.errorDebug(error, <Object> { })));
  }
}
