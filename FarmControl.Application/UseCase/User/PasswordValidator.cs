using FarmControl.Exceptions;
using FluentValidation;

namespace FarmControl.Communication.UseCases.User;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password).NotEmpty().WithMessage(ResourceMensagesError.SENHA_USER_EMBRANCO);
        RuleFor(password => password.Length).GreaterThanOrEqualTo(6).When(password => !string.IsNullOrWhiteSpace(password)).WithMessage(ResourceMensagesError.SENHA_USER_MINIMO_SEIS_CARACTERES);
    }
}
