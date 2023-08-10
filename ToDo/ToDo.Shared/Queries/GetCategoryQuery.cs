using MediatR;
using ToDo.Shared.Dtos;

namespace ToDo.Shared.Queries;

public record GetCategoryQuery(Guid Id) : IRequest<CategoryDto?>;