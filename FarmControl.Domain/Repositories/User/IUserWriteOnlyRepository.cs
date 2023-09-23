namespace FarmControl.Domain.Repositories;

public interface IUserWriteOnlyRepository
{
    Task Add(FarmControl.Domain.Entities.User user);
}
