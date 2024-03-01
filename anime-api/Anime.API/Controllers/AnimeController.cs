using Anime.API.Services;
using Anime.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly IAnimeService _animeService;

        public AnimeController(IAnimeService animeService)
        {
            _animeService = animeService;
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

            return CreatedAtAction(nameof(CreateAnimeDTO), new { id }, createAnimeDto);
        }

        [HttpPost("vote")]
        public async Task<ActionResult> AddVote([FromBody] int animeId)
        {
            await _animeService.AddAnimeVote(animeId);

            return NoContent();
        }
    }
}
