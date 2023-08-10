namespace ToDo.Host.Persistence.Entities;

public class CategoryEntity
{
    public CategoryEntity()
    {
    }

    public CategoryEntity(string name, string color)
    {
        Name = name;
        Color = color;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}
