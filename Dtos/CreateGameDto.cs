
using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record CreateGameDto(
[Required][MaxLength(50)] string Name,
[Required][Range(1, 100000)] double Price,
[Required] DateOnly ReleaseDate
);