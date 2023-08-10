namespace ToDo.Shared.Dtos;

public record NewTaskDto(string Title, string? Description, Guid? CategoryId);
