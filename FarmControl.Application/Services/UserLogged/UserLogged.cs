using FarmControl.Application.Services.Token;
using FarmControl.Domain.Entities;
using FarmControl.Domain.Repositories.User;
using Microsoft.AspNetCore.Http;

namespace FarmControl.Application.Services.UserLogged;

public class UserLogged : IUserLogged
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenController _tokenController;
    private readonly IUserReadOnlyRepository _repository;

    public UserLogged(IHttpContextAccessor httpContextAccessor, TokenController tokenController, IUserReadOnlyRepository repository)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenController = tokenController;
        _repository = repository;
    }
    public async Task<User> RecoverUser()
    {
        var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();

        var userEmail = _tokenController.RecoverEmail(token);

        var user = await _repository.RetrieveByEmail(userEmail);

        return user;
    }
}
