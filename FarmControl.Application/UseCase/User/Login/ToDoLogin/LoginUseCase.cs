using FarmControl.Application.Services.Cryptography;
using FarmControl.Application.Services.Token;
using FarmControl.Application.UseCases.User.Login.ToDoLogin;
using FarmControl.Communication.Request;
using FarmControl.Communication.Response;
using FarmControl.Domain.Repositories.User;
using FarmControl.Exceptions.ExceptionsBase;


namespace FarmControl.Communication.UseCases.User.Login.ToDoLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly PasswordEncryptor _passwordEncryptor;
    private readonly TokenController _tokenController;

    public LoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, PasswordEncryptor passwordEncryptor, TokenController tokenController)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncryptor = passwordEncryptor;
        _tokenController = tokenController;
    }
    public async Task<ResponseLoginJson> Execute(RequestLoginJson request)
    {
        var passwordEncryptor = _passwordEncryptor.Encrypt(request.Password);

        var user = await _userReadOnlyRepository.Login(request.Email, passwordEncryptor);

        if (user == null)
        {
            throw new InvalidLoginException();
        }

        return new ResponseLoginJson
        {
            Name = user.Name,
            Token = _tokenController.GenerateToken(user.Email)
        };
    }
}
