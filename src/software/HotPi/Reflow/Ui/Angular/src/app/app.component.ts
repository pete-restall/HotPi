import { Component } from '@angular/core';

import { RootViewModelService } from './root-view-model.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private rootViewModel: RootViewModelService) { }

  title = 'HotPi Reflow Oven Controller';

  public get viewModel(): RootViewModelService {
    return this.rootViewModel;
  }
}
