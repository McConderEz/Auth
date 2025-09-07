using Core.Validators;
using SharedKernel.Shared;
using SharedKernel.Shared.Errors;
using FluentValidation;

namespace Accounts.Application.AccountManagement.Commands.Refresh;

public class RefreshTokensCommandValidator: AbstractValidator<RefreshTokensCommand>
{
    public RefreshTokensCommandValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("refresh token"));
    }
}