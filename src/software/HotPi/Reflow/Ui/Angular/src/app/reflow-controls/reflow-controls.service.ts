import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { IHaveStartReflowLink, I_HAVE_START_REFLOW_LINK } from './i-have-start-reflow-link.interface';
import { CurrentReflow } from './current-reflow.class';

@Injectable()
export class ReflowControlsService {
  constructor(
    private httpClient: HttpClient,
    @Inject(I_HAVE_START_REFLOW_LINK) private links: IHaveStartReflowLink) {
  }

  public start(): CurrentReflow {
    // TODO: START /reflow, which returns an ID and a link to stop the reflow, a link to get the current status, etc. etc.
    this.httpClient.request(
      'START',
      this.links.startReflowLink,
      {
        responseType: 'json'
      }
    ).subscribe(data => console.log(data));

    return new CurrentReflow();
  }
}
