using MediatR;
using ToDo.Host.Persistence;
using ToDo.Host.Persistence.Entities;
using ToDo.Shared.Commands;
using ToDo.Shared.Enums;

namespace ToDo.Host.Handlers;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
{
    private readonly ToDoDbContext _context;

    public CreateTaskCommandHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var newTask = new TaskEntity(request.Title, request.Description, Status.ToDo, request.CategoryId);

        _context.Tasks.Add(newTask);
        await _context.SaveChangesAsync();
    }
}
