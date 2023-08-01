using Microsoft.JSInterop;
using System.Text.Json;
using ToDo.Shared.Enums;
using ToDo.Shared.Models;
using ToDo.Shared.Services;

namespace ToDo.Client.Shared.Services
{
    internal sealed class TaskService : ITaskService
    {
        private List<TaskModel> tasks = new List<TaskModel>();
        private List<Category> categories = new List<Category>();
        private readonly IJSRuntime runtime;

        public TaskService(IJSRuntime runtime)
        {
            this.runtime = runtime;
        }

        public async Task ChangeCategory(Guid taskId, Guid categoryId)
        {
            await LoadData();

            var category = categories.FirstOrDefault(c => c.Id == categoryId);
            var task = tasks.FirstOrDefault(c => c.Id == taskId);

            if (category is null || task is null)
                return;

            task = task with { Category = category };

            await SaveData();
        }

        public async Task Create(NewTask data)
        {
            await LoadData();

            var category = categories.FirstOrDefault(x => x.Id == data.CategoryId);

            var task = new TaskModel(Guid.NewGuid(), data.Title, data.Description, Status.ToDo, category);
            tasks.Add(task);

            await SaveData();
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            await LoadData();

            return tasks;
        }

        public async Task<IEnumerable<TaskModel>> GetWithStatus(Status status)
        {
            await LoadData();

            return tasks.Where(x => x.Status == status) ?? new List<TaskModel>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            await LoadData();

            return categories;
        }

        public async Task ChangeStatus(Guid taskId, Status newStatus)
        {
            await LoadData();

            var task = tasks.FirstOrDefault(x => x.Id == taskId);

            if (task is null) return;

            var index = tasks.IndexOf(task);

            task = task with { Status = newStatus };

            tasks[index] = task;

            await SaveData();
        }

        public async Task Remove(Guid taskId)
        {
            await LoadData();

            var taskToRemove = tasks.FirstOrDefault(x => x.Id == taskId);

            if (taskToRemove is null) return;

            tasks.Remove(taskToRemove);

            await SaveData();
        }

        public async Task Update(UpdateTask data)
        {
            await LoadData();

            var task = tasks.FirstOrDefault(x => x.Id == data.TaskId);

            if (task is null) return;

            var index = tasks.IndexOf(task);

            var category = categories.FirstOrDefault(x => x.Id == data.CategoryId);

            task = task with { Title = data.Title, Description = data.Description, Category = category };

            tasks[index] = task;

            await SaveData();
        }

        private async Task LoadData()
        {
            var loadedTasks = await runtime.InvokeAsync<string>("localStorage.getItem", "tasks");
            var loadedCategories = await runtime.InvokeAsync<string>("localStorage.getItem", "categories");

            if (loadedTasks is not null)
            {
                tasks = JsonSerializer.Deserialize<IEnumerable<TaskModel>>(loadedTasks).ToList();
            }

            if (loadedCategories is not null)
            {
                categories = JsonSerializer.Deserialize<IEnumerable<Category>>(loadedCategories).ToList();
            }
        }

        private async Task SaveData()
        {
            await runtime.InvokeAsync<IEnumerable<TaskModel>>("localStorage.setItem", "tasks", JsonSerializer.Serialize(tasks));
            await runtime.InvokeAsync<IEnumerable<Category>>("localStorage.setItem", "categories", JsonSerializer.Serialize(categories));
        }
    }
}
