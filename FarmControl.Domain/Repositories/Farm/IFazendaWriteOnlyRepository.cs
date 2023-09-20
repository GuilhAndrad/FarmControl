using FarmControl.Domain.Entities;

namespace FarmControl.Domain.Repositories.Farm;
public interface IFazendaWriteOnlyRepository
{
    Task Add(Fazenda fazenda);
}
