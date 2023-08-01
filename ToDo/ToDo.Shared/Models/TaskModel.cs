using ToDo.Shared.Enums;

namespace ToDo.Shared.Models;

public record TaskModel(Guid Id, string Title, string Description, Status Status, Category? Category);
