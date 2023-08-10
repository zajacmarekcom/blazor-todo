using ToDo.Shared.Dtos;
using ToDo.Shared.Enums;

namespace ToDo.Host.Persistence.Entities;

public class TaskEntity
{
    public TaskEntity()
    {
    }

    public TaskEntity(string title, string? description, Status status, Guid? categoryId)
    {
        Title = title;
        Description = description;
        Status = status;
        CategoryId = categoryId;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public Guid? CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
}
