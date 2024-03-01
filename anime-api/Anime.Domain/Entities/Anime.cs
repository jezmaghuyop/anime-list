namespace Anime.Domain.Entities
{
    public class Anime
    {        
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public long Votes { get; set; }
    }
}
