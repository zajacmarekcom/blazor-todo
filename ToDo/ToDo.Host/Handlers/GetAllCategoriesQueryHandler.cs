using MediatR;
using ToDo.Host.Persistence;
using ToDo.Shared.Dtos;
using ToDo.Shared.Queries;

namespace ToDo.Host.Handlers;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ToDoDbContext _context;

    public GetAllCategoriesQueryHandler(ToDoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = _context.Categories.Select(x => new CategoryDto(x.Id, x.Name, x.Color));

        return categories;
    }
}
