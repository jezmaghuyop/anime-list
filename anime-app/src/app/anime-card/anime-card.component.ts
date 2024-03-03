import {
  Component,
  computed,
  input,
  Output,
  EventEmitter,
} from '@angular/core';
import { Anime } from '@core/models';

@Component({
  selector: 'app-anime-card',
  standalone: true,
  imports: [],
  templateUrl: './anime-card.component.html',
  styleUrls: ['./anime-card.component.scss'],
})
export class AnimeCardComponent {
  context = input.required<Anime>();
  vote = computed(() => this.context().votes);

  @Output() voteEvent = new EventEmitter<number>();

  voteForAnime() {
    this.voteEvent.emit(this.context().id);
  }
}
