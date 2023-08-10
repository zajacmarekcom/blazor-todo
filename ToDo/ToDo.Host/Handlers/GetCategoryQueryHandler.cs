using MediatR;
using ToDo.Host.Persistence;
using ToDo.Shared.Dtos;
using ToDo.Shared.Queries;

namespace ToDo.Host.Handlers;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryDto?>
{
    private readonly ToDoDbContext _context;

    public GetCategoryQueryHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto?> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = _context.Categories.FirstOrDefault(x => x.Id == request.Id);

        return category is null
            ? null
            : new CategoryDto(category.Id, category.Name, category.Color);
    }
}
