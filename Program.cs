using GameStore.Api.Data;
using GameStore.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

string connectionString = "Data Source=Gamestore.db";

builder.Services.AddSqlite<GameStoreContext>(connectionString);


WebApplication app = builder.Build();

app.MigrateDb();

app.MapGamesEndpoints();

app.Run();
