using MediatR;

namespace ToDo.Shared.Commands;

public record EditCategoryCommand(Guid Id, string Name, string Color) : IRequest;
