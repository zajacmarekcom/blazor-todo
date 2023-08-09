using ToDo.Shared.Enums;
using ToDo.Shared.Models;

namespace ToDo.Shared.Services;

public interface ITaskService
{
    Task Create(NewTask data);
    Task CreateCategory(NewCategory data);
    Task Remove(Guid taskId);
    Task Update(UpdateTask data);
    Task UpdateCategory(UpdateCategory data);
    Task ChangeStatus(Guid taskId, Status newStatus);
    Task ChangeCategory(Guid taskId, Guid categoryId);
    Task<IEnumerable<TaskModel>> GetAll(Guid? withCategory);
    Task<IEnumerable<TaskModel>> GetWithStatus(Status status);
    Task<IEnumerable<Category>> GetCategories();
    Task<Category?> GetCategory(Guid categoryId);
}

