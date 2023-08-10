using MediatR;
using ToDo.Shared.Enums;

namespace ToDo.Shared.Commands;

public record ChangeTaskStatusCommand(Guid Id, Status NewStatus) : IRequest;
