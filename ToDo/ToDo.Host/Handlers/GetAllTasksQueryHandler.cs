using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Host.Persistence;
using ToDo.Host.Persistence.Entities;
using ToDo.Shared.Dtos;
using ToDo.Shared.Queries;

namespace ToDo.Host.Handlers;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskDto>>
{
    private readonly ToDoDbContext _context;

    public GetAllTasksQueryHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        IQueryable<TaskEntity> tasks = _context.Tasks.Include(x => x.Category);

        if (request.CategoryId is not null)
        {
            tasks = tasks.Where(x => x.CategoryId == request.CategoryId);
        }

        var dtos = tasks
            .Select(x => new TaskDto(
                x.Id,
                x.Title,
                x.Description,
                x.Status,
                x.Category == null ? null : new CategoryDto(x.Category.Id, x.Category.Name, x.Category.Color)));

        return dtos;
    }
}
