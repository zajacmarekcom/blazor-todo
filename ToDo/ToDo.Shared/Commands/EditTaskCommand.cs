using MediatR;

namespace ToDo.Shared.Commands;

public record EditTaskCommand(Guid Id, string Title, string? Description, Guid? CategoryId) : IRequest;