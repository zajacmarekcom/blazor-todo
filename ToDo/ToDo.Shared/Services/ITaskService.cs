using ToDo.Shared.Dtos;
using ToDo.Shared.Enums;

namespace ToDo.Shared.Services;

public interface ITaskService
{
    Task Create(NewTaskDto data);
    Task CreateCategory(NewCategoryDto data);
    Task Remove(Guid taskId);
    Task Update(UpdateTaskDto data);
    Task UpdateCategory(UpdateCategoryDto data);
    Task ChangeStatus(Guid taskId, Status newStatus);
    Task<IEnumerable<TaskDto>> GetAll(Guid? withCategory);
    Task<IEnumerable<CategoryDto>> GetCategories();
    Task<CategoryDto?> GetCategory(Guid categoryId);
}

