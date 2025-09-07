using Core.Validators;
using SharedKernel.Shared.Errors;
using FluentValidation;

namespace Accounts.Application.AccountManagement.Commands.DeleteRefreshSession;

public class DeleteRefreshTokenValidator: AbstractValidator<DeleteRefreshTokenCommand>
{
    public DeleteRefreshTokenValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("refresh token"));
    }
}