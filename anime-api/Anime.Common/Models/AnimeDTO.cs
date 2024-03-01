namespace Anime.Common.Models;

public class AnimeDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<AnimeVotesDTO> Votes { get; set; } = new();
}