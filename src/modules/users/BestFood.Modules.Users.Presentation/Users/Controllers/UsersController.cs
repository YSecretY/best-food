using BestFood.Modules.Users.Application.Users.Commands;
using BestFood.Modules.Users.Presentation.Users.Requests.Register;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BestFood.Modules.Users.Presentation.Users.Controllers;

[Route("api/v1/users")]
public class UsersController(
    ISender mediator,
    IMapper mapper
) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<RegisterUserCommand>(request);
        return Ok(await mediator.Send(command, cancellationToken));
    }
}