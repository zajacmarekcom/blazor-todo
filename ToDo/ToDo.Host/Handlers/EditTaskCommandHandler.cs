using MediatR;
using ToDo.Host.Persistence;
using ToDo.Shared.Commands;

namespace ToDo.Host.Handlers;

public class EditTaskCommandHandler : IRequestHandler<EditTaskCommand>
{
    private readonly ToDoDbContext _context;

    public EditTaskCommandHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditTaskCommand request, CancellationToken cancellationToken)
    {
        var task = _context.Tasks.FirstOrDefault(x => x.Id == request.Id);

        if (task is not null)
        {
            task.Title = request.Title;
            task.Description = request.Description;
            task.CategoryId = request.CategoryId;
        }
    }
}
