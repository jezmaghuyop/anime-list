export type Anime = {
    id: number;
    title: string;
    description: string;
    imageUrl: string;
    votes: AnimeVote[];
  };
  
  export type AnimeVote = {
    id: number;
    animeId: number;
  };
  