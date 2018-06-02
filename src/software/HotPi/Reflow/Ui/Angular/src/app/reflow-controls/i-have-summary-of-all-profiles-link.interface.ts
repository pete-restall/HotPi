import { InjectionToken } from '@angular/core';

export const I_HAVE_SUMMARY_OF_ALL_PROFILES_LINK =
  new InjectionToken<IHaveSummaryOfAllProfilesLink>('IHaveSummaryOfAllProfilesLink');

export interface IHaveSummaryOfAllProfilesLink {
  readonly summaryOfAllProfilesLink: string;
}
