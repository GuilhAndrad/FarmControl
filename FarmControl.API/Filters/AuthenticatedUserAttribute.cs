using FarmControl.Application.Services.Token;
using FarmControl.Communication.Response;
using FarmControl.Domain.Repositories.User;
using FarmControl.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace FarmControl.Api.Filters;

public class AuthenticatedUserAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUserReadOnlyRepository _repository;

    public AuthenticatedUserAttribute(TokenController tokenController, IUserReadOnlyRepository repository)
    {
        _tokenController = tokenController;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenRequest(context);
            var userEmail = _tokenController.RecoverEmail(token);
            var user = await _repository.RetrieveByEmail(userEmail);

            if (user is null)
            {
                throw new System.Exception();
            }
        }
        catch (SecurityTokenExpiredException)
        {

            ExpiredToken(context);
        }
        catch
        {
            UserWithoutPermission(context);
        }
    }

    private static string TokenRequest(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(authorization))
        {
            throw new System.Exception();
        }
        return authorization["Bearer".Length..].Trim();
    }

    private static void ExpiredToken(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ErrorResponseJson(ResourceMensagesError.TOKEN_EXPIRADO));
    }

    private static void UserWithoutPermission(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ErrorResponseJson(ResourceMensagesError.USER_NAO_PERMITIDO));
    }
}