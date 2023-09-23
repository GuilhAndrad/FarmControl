using AutoMapper;
using FarmControl.Application.Services.Cryptography;
using FarmControl.Application.Services.Token;
using FarmControl.Communication.Request;
using FarmControl.Communication.Response;
using FarmControl.Communication.UseCases.User.Register;
using FarmControl.Domain.Repositories;
using FarmControl.Domain.Repositories.User;
using FarmControl.Exceptions;
using FarmControl.Exceptions.ExceptionsBase;

namespace FarmControl.Application.UseCase.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unityOfWork;
    private readonly PasswordEncryptor _passwordEncryptor;
    private readonly TokenController _tokenController;
    public RegisterUserUseCase(IUserReadOnlyRepository userReadOnlyRepository, IUserWriteOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork,
        PasswordEncryptor passwordEncryptor, TokenController tokenController)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _repository = repository;
        _mapper = mapper;
        _unityOfWork = unitOfWork;
        _passwordEncryptor = passwordEncryptor;
        _tokenController = tokenController;
    }

    public async Task<ResponseRegisteredUserReplyJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var entitie = _mapper.Map<Domain.Entities.User>(request);
        entitie.Password = _passwordEncryptor.Encrypt(request.Password);

        await _repository.Add(entitie);

        await _unityOfWork.Commit();

        var token = _tokenController.GenerateToken(entitie.Email);

        return new ResponseRegisteredUserReplyJson
        {
            Token = token
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var userEmailExist = await _userReadOnlyRepository.ThereIsUserWithEmail(request.Email);
        if (userEmailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMensagesError.EMAIL_JA_REGISTRADO));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
