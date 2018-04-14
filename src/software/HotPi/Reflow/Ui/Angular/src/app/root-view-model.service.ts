import { Injectable } from '@angular/core';

import { IHaveSummaryOfAllProfilesLink } from './reflow-controls/i-have-summary-of-all-profiles-link.interface';

@Injectable()
export class RootViewModelService implements IHaveSummaryOfAllProfilesLink {
  public get shutdownLink(): string {
    return this.viewModel.pi.shutdownLink;
  }

  private get viewModel(): any {
    return this._window.hotpi.viewModel;
  }

  private get _window(): any {
    return window;
  }

  public get summaryOfAllProfilesLink(): string {
    return this.viewModel.profile.summaryOfAllProfilesLink;
  }
}
