namespace FarmControl.Application.Services.UserLogged;

public interface IUserLogged
{
    Task<Domain.Entities.User> RecoverUser();
}
