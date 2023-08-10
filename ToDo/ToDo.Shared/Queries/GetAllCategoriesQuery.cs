using MediatR;
using ToDo.Shared.Dtos;

namespace ToDo.Shared.Queries;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;