using MediatR;
using ToDo.Host.Persistence;
using ToDo.Shared.Dtos;
using ToDo.Shared.Queries;

namespace ToDo.Host.Handlers;

public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDto?>
{
    private readonly ToDoDbContext _context;

    public GetTaskQueryHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task<TaskDto?> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = _context.Tasks.FirstOrDefault(x => x.Id == request.Id);

        return task is null
            ? null
            : new TaskDto(
                task.Id,
                task.Title,
                task.Description,
                task.Status,
                task.Category is null
                    ? null
                    : new CategoryDto(task.Category.Id, task.Category.Name, task.Category.Color));
    }
}
