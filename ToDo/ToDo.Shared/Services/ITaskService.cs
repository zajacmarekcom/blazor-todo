using ToDo.Shared.Enums;
using ToDo.Shared.Models;

namespace ToDo.Shared.Services;

public interface ITaskService
{
    Task Create(NewTask data);
    Task Remove(Guid taskId);
    Task Update(UpdateTask data);
    Task ChangeStatus(Guid taskId, Status newStatus);
    Task ChangeCategory(Guid taskId, Guid categoryId);
    Task<IEnumerable<TaskModel>> GetAll();
    Task<IEnumerable<TaskModel>> GetWithStatus(Status status);
    Task<IEnumerable<Category>> GetCategories();
}

