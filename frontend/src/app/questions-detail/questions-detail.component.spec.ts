import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionsDetailComponent } from './questions-detail.component';

describe('QuestionsDetailComponent', () => {
  let component: QuestionsDetailComponent;
  let fixture: ComponentFixture<QuestionsDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestionsDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionsDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
