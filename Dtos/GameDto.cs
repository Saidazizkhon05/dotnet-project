namespace GameStore.Api.Dtos;

public record GameDto(
int Id,
string Name,
string Genre,
double Price,
DateOnly ReleaseDate
);

