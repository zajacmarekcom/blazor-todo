using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;
using ToDo.Shared.Commands;
using ToDo.Shared.Dtos;
using ToDo.Shared.Enums;
using ToDo.Shared.Services;

namespace ToDo.Client.Shared.Services;

internal sealed class TaskService : ITaskService
{
    private readonly HttpClient http;

    public TaskService(HttpClient http)
    {
        this.http = http;
    }

    public async Task ChangeStatus(Guid taskId, Status newStatus)
    {
        await http.PutAsJsonAsync<ChangeTaskStatusCommand>("task/status", new(taskId, newStatus));
    }

    public async Task Update(UpdateTaskDto data)
    {
        await http.PutAsJsonAsync<EditTaskCommand>("task", new(data.Id, data.Title, data.Description, data.CategoryId));
    }

    public async Task UpdateCategory(UpdateCategoryDto data)
    {
        await http.PutAsJsonAsync<EditCategoryCommand>("category", new(data.Id, data.Name, data.Color));
    }

    public async Task Create(NewTaskDto data)
    {
        await http.PostAsJsonAsync<CreateTaskCommand>("task", new(data.Title, data.Description, data.CategoryId));
    }

    public async Task CreateCategory(NewCategoryDto data)
    {
        await http.PostAsJsonAsync<CreateCategoryCommand>("category", new(data.Name, data.Color));
    }

    public async Task<IEnumerable<TaskDto>> GetAll(Guid? withCategory)
    {
        var url = withCategory is null ? "task" : $"task?categoryId={withCategory.ToString()}";

        var tasks = await http.GetFromJsonAsync<IEnumerable<TaskDto>>(url);

        return tasks ?? new List<TaskDto>();
    }

    public async Task<IEnumerable<CategoryDto>> GetCategories()
    {
        var categories = await http.GetFromJsonAsync<IEnumerable<CategoryDto>>("category");

        return categories ?? new List<CategoryDto>();
    }

    public async Task<CategoryDto?> GetCategory(Guid categoryId)
    {
        var category = await http.GetFromJsonAsync<CategoryDto>($"category/{categoryId.ToString()}");

        return category;
    }

    public async Task Remove(Guid taskId)
    {
        await http.DeleteAsync($"task/{taskId.ToString()}");
    }
}
