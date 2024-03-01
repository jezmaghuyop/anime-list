namespace Anime.Common.Entities;

public class AnimeVotes
{
    public int Id { get; set; }
    public int AnimeId { get; set; }

    public virtual Anime Anime { get; set; }
}
