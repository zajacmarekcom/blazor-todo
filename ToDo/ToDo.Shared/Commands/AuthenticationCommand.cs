using MediatR;
using ToDo.Shared.Dtos;

namespace ToDo.Shared.Commands;

public record AuthenticationCommand(string? Code) : IRequest<UserDto?>;
