import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListRepliesComponent } from './list-replies.component';

describe('ListRepliesComponent', () => {
  let component: ListRepliesComponent;
  let fixture: ComponentFixture<ListRepliesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListRepliesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListRepliesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
