import { Component, input } from '@angular/core';
import { Anime } from '@core/models';

@Component({
  selector: 'app-anime-card',
  standalone: true,
  imports: [],
  templateUrl: './anime-card.component.html',
  styleUrl: './anime-card.component.scss',
})
export class AnimeCardComponent {
   context = input.required<Anime>();
}
