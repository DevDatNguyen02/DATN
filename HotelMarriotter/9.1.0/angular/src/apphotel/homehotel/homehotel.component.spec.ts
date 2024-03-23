import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomehotelComponent } from './homehotel.component';

describe('HomehotelComponent', () => {
  let component: HomehotelComponent;
  let fixture: ComponentFixture<HomehotelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomehotelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HomehotelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
