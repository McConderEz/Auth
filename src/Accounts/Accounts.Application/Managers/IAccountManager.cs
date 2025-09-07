using Accounts.Domain;
using SharedKernel.Shared;

namespace Accounts.Application.Managers;

public interface IAccountManager
{
    Task CreateAdminAccount(AdminProfile adminProfile);

    Task<Result> CreateParticipantAccount(
        ParticipantAccount participantAccount, CancellationToken cancellationToken = default);
}