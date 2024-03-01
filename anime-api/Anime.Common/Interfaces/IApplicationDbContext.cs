using Anime.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Anime.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Entities.Anime> Animes { get; }

    DbSet<AnimeVotes> AnimeVotes { get; }
   
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}