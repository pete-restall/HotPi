import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs/Observable';

import { ProfileSummary } from './profile-summary.interface';
import { CurrentReflow } from './current-reflow.interface';
import { ReflowControlsService } from './reflow-controls.service';
import { SummaryOfAllProfiles } from './summary-of-all-profiles.interface';

@Component({
  selector: 'hotpi-reflow-controls',
  templateUrl: './reflow-controls.component.html',
  styleUrls: ['./reflow-controls.component.css']
})
export class ReflowControlsComponent implements OnInit {
  private selectedReflowProfile: ProfileSummary;
  private currentReflow: CurrentReflow;

  constructor(private service: ReflowControlsService) { }

  public ngOnInit(): void {
    this.summary = this.service.getSummaryOfAllProfiles();
  }

  public summary: Observable<SummaryOfAllProfiles>;

  public onReflowProfileChanged(profile: ProfileSummary): void {
    this.selectedReflowProfile = profile;
  }

  public startReflow(): void {
    if (!this.selectedReflowProfile)
      return;

    this.service.startReflow(this.selectedReflowProfile).subscribe(currentReflow => {
      if (!currentReflow.processRunId)
        return;

      this.currentReflow = currentReflow;
    });
  }

  public abortReflow(): void {
    if (!this.currentReflow || !this.currentReflow.processRunId)
      return;

    this.service.abortReflow(this.currentReflow).subscribe(_ => {
      this.currentReflow = null;
    });
  }
}
