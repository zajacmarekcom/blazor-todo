using MediatR;

namespace ToDo.Shared.Commands;

public record CreateCategoryCommand(string Name, string Color) : IRequest;
