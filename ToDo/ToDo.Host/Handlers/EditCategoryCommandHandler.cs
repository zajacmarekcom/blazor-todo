using MediatR;
using ToDo.Host.Persistence;
using ToDo.Shared.Commands;

namespace ToDo.Host.Handlers;

public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand>
{
    private readonly ToDoDbContext _context;

    public EditCategoryCommandHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _context.Categories.FirstOrDefault(x => x.Id == request.Id);

        if (category is not null)
        {
            category.Name = request.Name;
            category.Color = request.Color;
        }

        await _context.SaveChangesAsync();
    }
}
