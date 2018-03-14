import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OvenTemperatureSampledObservableComponent } from './oven-temperature-sampled-observable.component';

describe('OvenTemperatureSampledObservableComponent', () => {
  let component: OvenTemperatureSampledObservableComponent;
  let fixture: ComponentFixture<OvenTemperatureSampledObservableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OvenTemperatureSampledObservableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OvenTemperatureSampledObservableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
