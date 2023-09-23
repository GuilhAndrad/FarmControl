namespace FarmControl.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ThereIsUserWithEmail(string email);
    Task<FarmControl.Domain.Entities.User> Login(string email, string password);
    Task<FarmControl.Domain.Entities.User> RetrieveByEmail(string email);
}
