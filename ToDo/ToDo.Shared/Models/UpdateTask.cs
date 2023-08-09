namespace ToDo.Shared.Models;

public record UpdateTask(Guid TaskId, string Title, string? Description, Guid? CategoryId);
