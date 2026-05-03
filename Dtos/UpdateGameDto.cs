namespace GameStore.Api.Dtos;

public record UpdateGameDto(
    string Name,
    double Price,
    DateOnly ReleaseDate
);