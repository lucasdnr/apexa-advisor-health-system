import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditAdvisorComponent } from './edit-advisor.component';

describe('EditAdvisorComponent', () => {
  let component: EditAdvisorComponent;
  let fixture: ComponentFixture<EditAdvisorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditAdvisorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditAdvisorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
