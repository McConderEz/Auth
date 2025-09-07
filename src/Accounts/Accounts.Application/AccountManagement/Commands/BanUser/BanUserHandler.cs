using Accounts.Domain;
using Core.Abstractions;
using Core.Database;
using Core.Extension;
using SharedKernel.Constraints;
using SharedKernel.Shared;
using SharedKernel.Shared.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.AccountManagement.Commands.BanUser;

public class BanUserHandler : ICommandHandler<BanUserCommand>
{
    private readonly ILogger<BanUserHandler> _logger;
    private readonly IValidator<BanUserCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public BanUserHandler(
        ILogger<BanUserHandler> logger,
        IValidator<BanUserCommand> validator,
        [FromKeyedServices(Constraints.Context.Accounts)]
        IUnitOfWork unitOfWork,
        UserManager<User> userManager)
    {
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<Result> Handle(BanUserCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);
        if (user is null)
        {
            return Errors.General.NotFound();
        }

        user.BanUser();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation($"User {user.Id} Banned");

        return Result.Success();
    }
}