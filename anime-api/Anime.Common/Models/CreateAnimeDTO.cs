namespace Anime.Common.Models;

public class CreateAnimeDTO
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
}
