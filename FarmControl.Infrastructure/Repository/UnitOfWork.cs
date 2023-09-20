using FarmControl.Domain.Repositories;
using FarmControl.Infrastructure.AccessRepositories;

namespace FarmControl.Infrastructure.Repository;
public sealed class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly FarmControlContext _context;
    private bool _disposed;
    public UnitOfWork(FarmControlContext context)
    {
        _context = context;
    }
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }
}
