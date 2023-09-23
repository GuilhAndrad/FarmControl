using FarmControl.Communication.Request;
using FarmControl.Communication.UseCases.User;
using FluentValidation;

namespace FarmControl.Application.UseCases.User.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordUserJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(c => c.NewPassword).SetValidator(new PasswordValidator());
    }
}
