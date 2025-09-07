using Accounts.Application.Managers;
using Accounts.Domain;
using Core.Database;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Constraints;
using SharedKernel.Shared;

namespace Accounts.Infrastructure.IdentityManagers;

public class AccountManager(
    AccountsDbContext accountsDbContext,
    [FromKeyedServices(Constraints.Context.Accounts)] IUnitOfWork unitOfWork) : IAccountManager
{
    public async Task CreateAdminAccount(AdminProfile adminProfile)
    {
        await accountsDbContext.AdminProfiles.AddAsync(adminProfile);
        await unitOfWork.SaveChanges();
    }
    
    public async Task<Result> CreateParticipantAccount(
        ParticipantAccount participantAccount, CancellationToken cancellationToken = default)
    {
        await accountsDbContext.ParticipantAccounts.AddAsync(participantAccount,cancellationToken);
        await unitOfWork.SaveChanges(cancellationToken);

        return Result.Success();
    }
}