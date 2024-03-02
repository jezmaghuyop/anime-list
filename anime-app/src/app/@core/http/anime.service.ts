import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '@env/environment';
import { Anime, AnimeVote } from '@core/models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AnimeService {
  private readonly http: HttpClient = inject(HttpClient);
  private readonly apiUrl = `${environment.api.baseUrl}${environment.api.endpoints.anime}`;

  constructor() {}

  public getAll(): Observable<Anime[]> {
    return this.http.get<Anime[]>(this.apiUrl);
  }

  public create(title: string, description: string, imageUrl: string) {
    return this.http.post(this.apiUrl, { title, description, imageUrl });
  }

  public vote(id: number): Observable<AnimeVote> {
    return this.http.post<AnimeVote>(`${this.apiUrl}/vote`, id);
  }
}
