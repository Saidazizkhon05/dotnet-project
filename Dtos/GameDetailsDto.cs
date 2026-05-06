namespace GameStore.Api.Dtos;

public record GameDetailsDto(
int Id,
string Name,
int GenreId,
double Price,
DateOnly ReleaseDate
);

