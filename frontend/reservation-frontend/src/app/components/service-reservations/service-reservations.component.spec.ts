import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceReservationsComponent } from './service-reservations.component';

describe('ServiceReservationsComponent', () => {
  let component: ServiceReservationsComponent;
  let fixture: ComponentFixture<ServiceReservationsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ServiceReservationsComponent]
    });
    fixture = TestBed.createComponent(ServiceReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
