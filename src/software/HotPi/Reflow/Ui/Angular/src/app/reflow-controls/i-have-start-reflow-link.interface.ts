import { InjectionToken } from '@angular/core';

export const I_HAVE_START_REFLOW_LINK = new InjectionToken<IHaveStartReflowLink>('IHaveStartReflowLink');

export interface IHaveStartReflowLink {
  readonly startReflowLink: string;
}
