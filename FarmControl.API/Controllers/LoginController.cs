using FarmControl.Application.UseCases.User.Login.ToDoLogin;
using FarmControl.Communication.Request;
using FarmControl.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace FarmControl.Api.Controllers
{
    public class LoginController : FarmControlController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromServices] ILoginUseCase useCase,
            [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
    }
}
