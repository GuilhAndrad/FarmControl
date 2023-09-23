using FarmControl.Communication.Request;

namespace FarmControl.Application.UseCases.User.ChangePassword;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordUserJson request);
}
