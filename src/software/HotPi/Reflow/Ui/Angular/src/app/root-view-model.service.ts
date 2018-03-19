import { Injectable } from '@angular/core';

@Injectable()
export class RootViewModelService {
  public get shutdownLink(): string {
    return this.viewModel.shutdownLink;
  }

  private get viewModel(): any {
    return this._window.hotpi.viewModel;
  }

  private get _window(): any {
    return window;
  }
}
