import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AnimeCardComponent } from './anime-card/anime-card.component';
import { Anime } from '@core/models';
import { AnimeService } from '@core/http';
import { untilDestroyed } from '@utilities/until-destroyed';
import { HttpClientModule } from '@angular/common/http';

type Context = {
  animes: Anime[];
};

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AnimeCardComponent, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [AnimeService],
})
export class AppComponent implements OnInit {
  context: Context = {
    animes: [],
  };

  private readonly _animeService: AnimeService = inject(AnimeService);
  private readonly takeUntilDestroyed = untilDestroyed();
  private hasVoted = false;

  ngOnInit(): void {
    this._animeService
      .getAll()
      .pipe(this.takeUntilDestroyed())
      .subscribe((animes) => {
        console.log(animes);
        this.context.animes = animes;
      });

    this.hasVoted = localStorage.getItem('hasVoted') === 'true';
  }

  onVote(id: number) {
    if (this.hasVoted) {
      alert('You have already voted!');

      return;
    }

    this._animeService
      .vote(id)
      .pipe(this.takeUntilDestroyed())
      .subscribe((result) => {
        this.hasVoted = true;
        localStorage.setItem('hasVoted', 'true');

        const anime = this.context.animes.find((a) => a.id === result.animeId);
        if (anime) {
          anime.votes.push({ id: result.id, animeId: anime.id });
        }
      });
  }
}
