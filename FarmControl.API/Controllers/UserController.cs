using FarmControl.Api.Filters;
using FarmControl.Application.UseCases.User.ChangePassword;
using FarmControl.Communication.Request;
using FarmControl.Communication.Response;
using FarmControl.Communication.UseCases.User.Register;
using Microsoft.AspNetCore.Mvc;

namespace FarmControl.Api.Controllers;

public class UserController : FarmControlController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserReplyJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpPut]
    [Route("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePasswordUserJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }
}