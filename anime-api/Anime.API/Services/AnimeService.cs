using Anime.Common.Models;
using Anime.Infrastructure.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Anime.API.Services
{
    public interface IAnimeService
    {
        Task<int> AddAnime(CreateAnimeDTO createAnime);
        Task<IEnumerable<Common.Models.AnimeDTO>> GetAnimes();
        Task<int> AddAnimeVote(int animeId);
    }

    public class AnimeService : IAnimeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AnimeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddAnime(CreateAnimeDTO createAnime)
        {
            var anime = _mapper.Map<Common.Entities.Anime>(createAnime);
            _context.Animes.Add(anime);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<AnimeDTO>> GetAnimes()
        {
            var animes = await _context.Animes.Include(x => x.Votes)
                 .ProjectTo<AnimeDTO>(_mapper.ConfigurationProvider).ToListAsync();

            return animes;
        }

        public async Task<int> AddAnimeVote(int animeId)
        {
            var animeVotes = new Common.Entities.AnimeVotes
            {
                AnimeId = animeId
            };

            _context.AnimeVotes.Add(animeVotes);

           var result = await _context.SaveChangesAsync();

            //var sql = @"INSERT INTO AnimeVotes (AnimeId) VALUES (@AnimeId);";
            //var result = await _context.Database.ExecuteSqlRawAsync(sql, new SqlParameter("@AnimeId", animeId));

            return result;
        }
    }
}
