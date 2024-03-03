import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnimeGraphComponent } from './anime-graph.component';

describe('AnimeGraphComponent', () => {
  let component: AnimeGraphComponent;
  let fixture: ComponentFixture<AnimeGraphComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [AnimeGraphComponent]
    });
    fixture = TestBed.createComponent(AnimeGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
