namespace GameStore.Api.Dtos;

public record GameDto(
int Id,
string Name,
double Price,
DateOnly ReleaseDate
);

