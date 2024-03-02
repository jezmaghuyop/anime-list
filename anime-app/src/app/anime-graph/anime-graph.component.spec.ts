import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnimeGraphComponent } from './anime-graph.component';

describe('AnimeGraphComponent', () => {
  let component: AnimeGraphComponent;
  let fixture: ComponentFixture<AnimeGraphComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AnimeGraphComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AnimeGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
