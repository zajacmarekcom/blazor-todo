using MediatR;
using ToDo.Host.Persistence;
using ToDo.Shared.Commands;

namespace ToDo.Host.Handlers;

public class RemoveTaskCommandHandler : IRequestHandler<RemoveTaskCommand>
{
    private readonly ToDoDbContext _context;

    public RemoveTaskCommandHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        var task = _context.Tasks.FirstOrDefault(x => x.Id == request.Id);

        if (task is null)
        {
            return;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}
