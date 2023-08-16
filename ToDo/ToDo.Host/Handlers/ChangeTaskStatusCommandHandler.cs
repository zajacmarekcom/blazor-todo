using MediatR;
using ToDo.Host.Persistence;
using ToDo.Shared.Commands;

namespace ToDo.Host.Handlers;

public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand>
{
    private readonly ToDoDbContext _context;

    public ChangeTaskStatusCommandHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = _context.Tasks.FirstOrDefault(x => x.Id ==  request.Id);

        if (task is null)
        {
            return;
        }

        task.Status = request.NewStatus;

        await _context.SaveChangesAsync();
    }
}
