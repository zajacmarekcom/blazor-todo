using ToDo.Shared.Enums;

namespace ToDo.Shared.Dtos;

public record TaskDto(Guid Id, string Title, string? Description, Status Status, CategoryDto? Category);
