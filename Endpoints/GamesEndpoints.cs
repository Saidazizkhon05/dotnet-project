using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using SQLitePCL;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
    new (1,
        "Pubg Mobile",
        "Shooter",
        19.99,
        new DateOnly(2000, 7,1)
    ),
    new (2,
            "Free Fire",
            "Shooter",
            0.00,
            new DateOnly(2021, 7,1)
        ),
    new (3,
            "Real Racing 3",
            "Racing",
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
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            GameDetailsDto gameDetails = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = gameDetails.Id },
                gameDetails);

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
                updatedGame.Genre,
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