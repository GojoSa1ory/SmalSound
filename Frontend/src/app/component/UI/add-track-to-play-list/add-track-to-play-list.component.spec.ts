import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTrackToPlayListComponent } from './add-track-to-play-list.component';

describe('AddTrackToPlayListComponent', () => {
  let component: AddTrackToPlayListComponent;
  let fixture: ComponentFixture<AddTrackToPlayListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddTrackToPlayListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddTrackToPlayListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
