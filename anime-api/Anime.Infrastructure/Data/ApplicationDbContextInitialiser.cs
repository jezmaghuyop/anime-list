using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Anime.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Animes.Any())
        {
            _context.Animes.Add(new Common.Entities.Anime
            {
                Title = "Frieren: Beyond the Journey's End",
                Description = "The mage Frieren defeated the Demon King alongside the hero Himmel's party after a 10-year quest. Peace was restored to the kingdom. Because she is an elf, she is able to live over a thousand years. She promises Himmel and the others that she will be back to see them and then sets out on a journey by herself.",
                ImageUrl = "/assets/images/freiren.jpg"
            });

            _context.Animes.Add(new Common.Entities.Anime
            {
                Title = "Mushoku Tensei: Jobless Reincarnation",
                Description = "A 34-year-old Japanese NEET is run over by a speeding truck and dies. Before he knows it, he is reborn as Rudeus Greyrat, and begins a new life full of adventure.",
                ImageUrl = "/assets/images/jobless.jpg"
            });

            _context.Animes.Add(new Common.Entities.Anime
            {
                Title = "That Time I Got Reincarnated as a Slime",
                Description = "The story follows Satoru Mikami, a salaryman who is murdered and then reincarnated in a sword and sorcery world as a titular slime, who goes on to gather allies to build his own nation of monsters. It was serialized online from 2013 to 2016 on the user-generated novel publishing website Shōsetsuka ni Narō.",
                ImageUrl = "/assets/images/slime.jpg"
            });

            await _context.SaveChangesAsync();
        }
    }
}