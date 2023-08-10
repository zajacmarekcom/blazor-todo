using MediatR;
using ToDo.Shared.Dtos;

namespace ToDo.Shared.Queries;

public record GetTaskQuery(Guid Id) : IRequest<TaskDto?>;