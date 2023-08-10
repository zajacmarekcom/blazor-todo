using MediatR;
using ToDo.Host.Persistence;
using ToDo.Host.Persistence.Entities;
using ToDo.Shared.Commands;

namespace ToDo.Host.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
    private readonly ToDoDbContext _context;

    public CreateCategoryCommandHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CategoryEntity(request.Name, request.Color);

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }
}
