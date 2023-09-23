using FarmControl.Communication.Request;
using FarmControl.Communication.UseCases.User;
using FarmControl.Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FarmControl.Application.UseCase.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage(ResourceMensagesError.NOME_USER_EMBRANCO);
        RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceMensagesError.EMAIL_USER_EMBRANCO);
        RuleFor(c => c.Phone).NotEmpty().WithMessage(ResourceMensagesError.TELEFONE_USER_EMBRANCO);
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(e => e.Email).EmailAddress().WithMessage(ResourceMensagesError.EMAIL_USER_INVALIDO);
        });


        When(c => !string.IsNullOrEmpty(c.Phone), () =>
        {
            RuleFor(c => c.Phone).Custom((phone, context) =>
            {
                string standardPhone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(phone, standardPhone);
                if (!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(phone), ResourceMensagesError.TELEFONE_USER_INVALIDO));
                }
            });
        });
    }
}
