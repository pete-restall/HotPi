import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReflowControlsComponent } from './reflow-controls.component';

describe('ReflowControlsComponent', () => {
  let component: ReflowControlsComponent;
  let fixture: ComponentFixture<ReflowControlsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReflowControlsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReflowControlsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
