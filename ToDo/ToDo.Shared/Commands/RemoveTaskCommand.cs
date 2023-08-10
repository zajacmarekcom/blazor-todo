using MediatR;

namespace ToDo.Shared.Commands;

public record RemoveTaskCommand(Guid Id) : IRequest;
