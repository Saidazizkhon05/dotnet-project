
namespace GameStore.Api.Dtos;

public record CreateGameDto(
string Name,
double Price,
DateOnly ReleaseDate
);