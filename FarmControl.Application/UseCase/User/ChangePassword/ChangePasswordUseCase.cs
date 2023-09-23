

using FarmControl.Application.Services.Cryptography;
using FarmControl.Application.Services.UserLogged;
using FarmControl.Communication.Request;
using FarmControl.Domain.Repositories;
using FarmControl.Domain.Repositories.User;
using FarmControl.Exceptions;
using FarmControl.Exceptions.ExceptionsBase;

namespace FarmControl.Application.UseCases.User.ChangePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly IUserLogged _userLogged;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly PasswordEncryptor _passwordEncryptor;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePasswordUseCase(IUserUpdateOnlyRepository repository, IUserLogged userLogged, PasswordEncryptor passwordEncryptor, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _userLogged = userLogged;
        _passwordEncryptor = passwordEncryptor;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestChangePasswordUserJson request)
    {
        var userLogged = await _userLogged.RecoverUser();
        var user = await _repository.RecoverById(userLogged.Id);

        Validate(request, user);

        user.Password = _passwordEncryptor.Encrypt(request.NewPassword);
        _repository.Update(user);
        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordUserJson request, Domain.Entities.User user)
    {
        var validator = new ChangePasswordValidator();
        var result = validator.Validate(request);

        var currentPasswordEncrypted = _passwordEncryptor.Encrypt(request.CurrentPassword);

        if (!user.Password.Equals(currentPasswordEncrypted))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("currentPassword", ResourceMensagesError.SENHA_ATUAL_INVALIDA));
        }
        if (!result.IsValid)
        {
            var mensages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ValidationErrorsException(mensages);
        }
    }
}
