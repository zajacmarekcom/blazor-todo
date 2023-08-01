using System.ComponentModel.DataAnnotations;

namespace ToDo.Shared.Models;

public record NewTask(string Title, string? Description, Guid? CategoryId);
