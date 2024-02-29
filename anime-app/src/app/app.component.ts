import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AnimeCardComponent } from './anime-card/anime-card.component';
import { Anime } from '@core/models';

type Context = {
  animes: Anime[];
};

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AnimeCardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  context: Context = {
    animes: [],
  };
}
