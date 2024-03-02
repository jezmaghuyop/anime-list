import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Anime, AnimeVote } from '@core/models';
import { AnimeService, RealtimeAnimeService } from '@core/http';
import { untilDestroyed } from '@utilities/until-destroyed';
import { HttpClientModule } from '@angular/common/http';
import { AnimeGraphComponent } from './anime-graph/anime-graph.component';
import { AnimeCardComponent } from './anime-card/anime-card.component';

type Context = {
  animes: Anime[];
};

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    AnimeCardComponent,
    HttpClientModule,
    AnimeGraphComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [AnimeService],
})
export class AppComponent implements OnInit {
  context: Context = {
    animes: [],
  };

  private readonly _animeService: AnimeService = inject(AnimeService);
  private readonly _realTimeAnimeService: RealtimeAnimeService =
    inject(RealtimeAnimeService);

  private readonly takeUntilDestroyed = untilDestroyed();
  private hasVoted = false;
  private recentlyAdded!: AnimeVote;

  ngOnInit(): void {
    this._realTimeAnimeService.startConnection();

    this._animeService
      .getAll()
      .pipe(this.takeUntilDestroyed())
      .subscribe((animes) => {
        this.context.animes = animes;
      });

    this.hasVoted = localStorage.getItem('hasVoted') === 'true';

    this._realTimeAnimeService.hubConnection.on(
      'AddNewVote',
      (result: AnimeVote) => {
        const anime = this.context.animes.find((a) => a.id === result.animeId);

        if (anime && this.recentlyAdded?.id !== result.id) {
          this.renderAnime(anime, result);
        }
      }
    );
  }

  onVote(id: number) {
    if (this.hasVoted) {
      alert('You have already voted!');

      return;
    }

    const anime = this.context.animes.find((a) => a.id === id);

    if (anime) {
      this._animeService
        .vote(id)
        .pipe(this.takeUntilDestroyed())
        .subscribe((result) => {
          this.hasVoted = true;
          localStorage.setItem('hasVoted', 'true');

          // Store the recently added vote, use this as a reference to prevent duplicate votes
          this.recentlyAdded = result;

          this.renderAnime(anime, result);
        });
    }
  }

  renderAnime(anime: Anime, data: AnimeVote) {
    // remove anime
    this.context.animes = this.context.animes.filter((a) => a.id !== anime.id);

    // re-add anime
    anime.votes.push({ id: data.id, animeId: data.id });

    const animes = [...this.context.animes];

    animes.push(anime);

    animes.sort((a, b) => a.id - b.id);

    this.context.animes = animes;
  }
}
