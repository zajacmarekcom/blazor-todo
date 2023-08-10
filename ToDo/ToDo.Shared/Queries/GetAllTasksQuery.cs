using MediatR;
using ToDo.Shared.Dtos;

namespace ToDo.Shared.Queries;

public record GetAllTasksQuery(Guid? CategoryId) : IRequest<IEnumerable<TaskDto>>;