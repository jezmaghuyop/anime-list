using Anime.Common.Entities;
using Anime.Common.Models;

namespace Anime.Common.Profile;

public class AnimeProfile : AutoMapper.Profile
{
    public AnimeProfile()
    {
        CreateMap<CreateAnimeDTO, Entities.Anime>();

        CreateMap<Entities.Anime, AnimeDTO>()
           .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes));

        CreateMap<AnimeVotes, AnimeVotesDTO>();

        CreateMap<CreateAnimeDTO, AnimeDTO>();
    }
}
