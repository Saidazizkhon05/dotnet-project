
using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record CreateGameDto(
[Required][MaxLength(50)] string Name,
[Range(1, 10)] int GenreId,
[Required][Range(1, 100000)] double Price,
[Required] DateOnly ReleaseDate
);