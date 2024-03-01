namespace Anime.Common.Entities;

public class Anime
{
    public Anime()
    {
        this.Votes = new HashSet<AnimeVotes>();
    }

    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
 
    public virtual ICollection<AnimeVotes> Votes { get; set; }
}
