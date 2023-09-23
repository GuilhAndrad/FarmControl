using FarmControl.Communication.Request;
using FarmControl.Communication.Response;

namespace FarmControl.Application.UseCases.User.Login.ToDoLogin;

public interface ILoginUseCase
{
    Task<ResponseLoginJson> Execute(RequestLoginJson request);
}
