import { TestBed, inject } from '@angular/core/testing';

import { RootViewModelService } from './root-view-model.service';

describe('RootViewModelService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RootViewModelService]
    });
  });

  it('should be created', inject([RootViewModelService], (service: RootViewModelService) => {
    expect(service).toBeTruthy();
  }));
});
