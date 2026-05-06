using GameStore.Api.Data;
using GameStore.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.Services.AddScoped<GameStoreContext>();
builder.AddGameStoreDb();

WebApplication app = builder.Build();

app.MigrateDb();

app.MapGamesEndpoints();

app.Run();
