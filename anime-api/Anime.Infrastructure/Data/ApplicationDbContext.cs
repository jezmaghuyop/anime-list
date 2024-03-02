using System.Reflection;
using System.Reflection.Emit;
using Anime.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Anime.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Common.Entities.Anime> Animes => Set<Common.Entities.Anime>();

        public DbSet<Common.Entities.AnimeVotes> AnimeVotes => Set<Common.Entities.AnimeVotes>();
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Common.Entities.AnimeVotes>()
                .ToTable(t => t.HasTrigger("brokerSender"));

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
