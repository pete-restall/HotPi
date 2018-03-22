import { Injectable } from '@angular/core';

import { IHaveStartReflowLink } from './reflow-controls/i-have-start-reflow-link.interface';

@Injectable()
export class RootViewModelService implements IHaveStartReflowLink {
  public get shutdownLink(): string {
    return this.viewModel.pi.shutdownLink;
  }

  private get viewModel(): any {
    return this._window.hotpi.viewModel;
  }

  private get _window(): any {
    return window;
  }

  public get startReflowLink(): string {
    return this.viewModel.reflow.startLink;
  }
}
