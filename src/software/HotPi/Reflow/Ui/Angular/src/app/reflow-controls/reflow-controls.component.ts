import { Component } from '@angular/core';

import { ReflowControlsService } from './reflow-controls.service';

@Component({
  selector: 'hotpi-reflow-controls',
  templateUrl: './reflow-controls.component.html',
  styleUrls: ['./reflow-controls.component.css']
})
export class ReflowControlsComponent {
  constructor(private service: ReflowControlsService) { }

  public start(): void {
    this.service.start();
  }
}
