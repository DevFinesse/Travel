import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TourCatalog } from './tour-catalog';

describe('TourCatalog', () => {
  let component: TourCatalog;
  let fixture: ComponentFixture<TourCatalog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TourCatalog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TourCatalog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
