import { TestBed, inject } from '@angular/core/testing';

import { ReflowControlsService } from './reflow-controls.service';

describe('ReflowControlsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReflowControlsService]
    });
  });

  it('should be created', inject([ReflowControlsService], (service: ReflowControlsService) => {
    expect(service).toBeTruthy();
  }));
});
