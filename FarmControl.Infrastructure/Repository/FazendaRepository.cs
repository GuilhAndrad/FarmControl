using FarmControl.Domain.Entities;
using FarmControl.Domain.Repositories.Farm;
using FarmControl.Infrastructure.AccessRepositories;

namespace FarmControl.Infrastructure.Repository;
public class FazendaRepository : IFazendaWriteOnlyRepository
{
    private readonly FarmControlContext _context;
    public FazendaRepository(FarmControlContext context)
    {
        _context = context;
    }
    public async Task Add(Fazenda fazenda)
    {
        await _context.Fazendas.AddAsync(fazenda);
    }
}
