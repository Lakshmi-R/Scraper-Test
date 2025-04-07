import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WebscraperComponent } from './webscraper.component';

describe('WebscraperComponent', () => {
  let component: WebscraperComponent;
  let fixture: ComponentFixture<WebscraperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WebscraperComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WebscraperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
