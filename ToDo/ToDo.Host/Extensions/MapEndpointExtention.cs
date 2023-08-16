using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Shared.Commands;
using ToDo.Shared.Queries;

namespace ToDo.Host.Extensions
{
    public static class MapEndpointExtention
    {
        public static WebApplication MapApi(this WebApplication webApplication)
        {
            var group = webApplication.MapGroup("/api");
            group.RequireAuthorization();

            group.MapGet("category/{id}",
                async
                ([FromRoute] Guid id,
                [FromServices] IMediator mediator) =>
                {
                    var query = new GetCategoryQuery(id);
                    return Results.Ok(await mediator.Send(query));
                });
            group.MapGet("category",
                async
                ([FromServices] IMediator mediator) =>
                {
                    var query = new GetAllCategoriesQuery();
                    return Results.Ok(await mediator.Send(query));
                });
            group.MapGet("task/{id}",
                async
                ([FromRoute] Guid id,
                [FromServices] IMediator mediator) =>
                {
                    var query = new GetTaskQuery(id);
                    return Results.Ok(await mediator.Send(query));
                });
            group.MapGet("task",
                async
                ([FromQuery] Guid? categoryId,
                    [FromServices] IMediator mediator) =>
                {
                    var query = new GetAllTasksQuery(categoryId);
                    return Results.Ok(await mediator.Send(query));
                });
            group.MapDelete("task/{id}",
                async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
                {
                    var command = new RemoveTaskCommand(id);
                    await mediator.Send(command);
                });
            group.MapPost("task",
                async ([FromBody] CreateTaskCommand command, [FromServices] IMediator mediator) =>
            await mediator.Send(command));
            group.MapPut("task",
                async ([FromBody] EditTaskCommand command, [FromServices] IMediator mediator) =>
            await mediator.Send(command));
            group.MapPut("task/status",
                async ([FromBody] ChangeTaskStatusCommand command, [FromServices] IMediator mediator) =>
            await mediator.Send(command));
            group.MapPost("category",
                async ([FromBody] CreateCategoryCommand command, [FromServices] IMediator mediator) =>
            await mediator.Send(command));
            group.MapPut("category",
                async ([FromBody] EditCategoryCommand command, [FromServices] IMediator mediator) =>
            await mediator.Send(command));

            var loginGroup = webApplication.MapGroup("/api");
            loginGroup.MapPost("authorize", async (AuthenticationCommand command, [FromServices] IMediator mediator) =>
            await mediator.Send(command));

            return webApplication;
        }
    }
}
