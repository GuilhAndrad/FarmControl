using FarmControl.Domain.Entities;
using FarmControl.Domain.Repositories;
using FarmControl.Domain.Repositories.User;
using FarmControl.Infrastructure.AccessRepositories;
using Microsoft.EntityFrameworkCore;

namespace FarmControl.Infrastructure.Repository;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly FarmControlContext _context;
    public UserRepository(FarmControlContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> ThereIsUserWithEmail(string email)
    {
        return await _context.Users.AnyAsync(c => c.Email.Equals(email));
    }

    public async Task<User> Login(string email, string password)
    {
        return await _context.Users.AsNoTracking()
             .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Password.Equals(password));
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public async Task<User> RetrieveByEmail(string email)
    {
        return await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email.Equals(email));
    }

    public async Task<User> RecoverById(long id)
    {
        return await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
    }
}
