using MediatR;

namespace ToDo.Shared.Commands;

public record CreateTaskCommand(string Title, string? Description, Guid? CategoryId) : IRequest;