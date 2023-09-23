using FarmControl.Communication.Request;
using FarmControl.Communication.Response;

namespace FarmControl.Communication.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserReplyJson> Execute(RequestRegisterUserJson request);
}
