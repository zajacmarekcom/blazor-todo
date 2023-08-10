namespace ToDo.Shared.Dtos;

public record UpdateTaskDto(Guid Id, string Title, string? Description, Guid? CategoryId);
