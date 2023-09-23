namespace FarmControl.Domain.Repositories.User;

public interface IUserUpdateOnlyRepository
{
    void Update(FarmControl.Domain.Entities.User user);
    Task<FarmControl.Domain.Entities.User> RecoverById(long id);
}
