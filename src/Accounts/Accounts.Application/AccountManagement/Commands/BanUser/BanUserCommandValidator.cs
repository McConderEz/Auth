using Core.Validators;
using SharedKernel.Shared.Errors;
using FluentValidation;

namespace Accounts.Application.AccountManagement.Commands.BanUser;

public class BanUserCommandValidator : AbstractValidator<BanUserCommand>
{
    public BanUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("user id"));
    }
}