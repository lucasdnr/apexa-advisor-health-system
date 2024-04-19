import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewAdvisorComponent } from './new-advisor.component';

describe('NewAdvisorComponent', () => {
  let component: NewAdvisorComponent;
  let fixture: ComponentFixture<NewAdvisorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewAdvisorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NewAdvisorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
