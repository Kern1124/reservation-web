import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceManagementModalComponent } from './service-management-modal.component';

describe('ServiceManagementModalComponent', () => {
  let component: ServiceManagementModalComponent;
  let fixture: ComponentFixture<ServiceManagementModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ServiceManagementModalComponent]
    });
    fixture = TestBed.createComponent(ServiceManagementModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
