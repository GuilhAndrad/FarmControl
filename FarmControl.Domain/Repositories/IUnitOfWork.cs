namespace FarmControl.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
