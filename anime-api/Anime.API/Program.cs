using Anime.API.Hubs;
using Anime.API.Subscriptions;
using Anime.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
// app.MapControllers();
app.UseEndpoints(endpoints =>
{    
    endpoints.MapHub<AnimeHub>("/hub/anime");
    endpoints.MapControllers();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Subscribe to table dependency 
app.UseSqlTableDependency<AnimeVotesSubscription>(connectionString);


app.Run();
