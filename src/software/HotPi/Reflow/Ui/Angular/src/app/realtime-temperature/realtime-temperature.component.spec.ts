import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RealtimeTemperatureComponent } from './realtime-temperature.component';

describe('RealtimeTemperatureComponent', () => {
  let component: RealtimeTemperatureComponent;
  let fixture: ComponentFixture<RealtimeTemperatureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RealtimeTemperatureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RealtimeTemperatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
