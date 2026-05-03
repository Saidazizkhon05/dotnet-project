using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
    new (1,
        "Pubg Mobile",
        19.99,
        new DateOnly(2000, 7,1)
    ),
    new (2,
            "Free Fire",
            0.00,
            new DateOnly(2021, 7,1)
        ),
    new (3,
            "Real Racing 3",
            99.99,
            new DateOnly(2005, 7,1)
        ),
    ];


    public static void MapGamesEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/games");

        // GET /games 
        group.MapGet("/", () =>
        {
            return Results.Ok(games);
        });

        // GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
            .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Price,
                newGame.ReleaseDate
                );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);

        });

        // PUT //games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            int index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        }
        );

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            int index = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            games.RemoveAt(index);

            return Results.NoContent();
        });
    }
} 