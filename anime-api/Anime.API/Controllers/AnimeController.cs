using Anime.API.Services;
using Anime.Common.Entities;
using Anime.Common.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService _animeService;
        private readonly IMapper _mapper;

        public AnimeController(IAnimeService animeService, IMapper mapper)
        {
            _animeService = animeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeDTO>>> GetAnimes()
        {
            var animes = await _animeService.GetAnimes();
            return Ok(animes);
        }

        [HttpPost]
        public async Task<ActionResult<AnimeDTO>> CreateAnime(CreateAnimeDTO createAnimeDto)
        {
            var id = await _animeService.AddAnime(createAnimeDto);
            var anime = _mapper.Map<AnimeDTO>(createAnimeDto);
            anime.Id = id;

            return Ok(anime);
        }

        [HttpPost("vote")]
        public async Task<ActionResult<AnimeVotesDTO>> AddVote([FromBody] int animeId)
        {
            var voteId = await _animeService.AddAnimeVote(animeId);

            var animeVote = new AnimeVotesDTO
            {
                Id = voteId,
                AnimeId = animeId
            };

            return Ok(animeVote);
        }
    }
}
