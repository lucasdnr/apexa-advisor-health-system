import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvisorFormComponent } from './advisor-form.component';

describe('AdvisorFormComponent', () => {
  let component: AdvisorFormComponent;
  let fixture: ComponentFixture<AdvisorFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdvisorFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdvisorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
