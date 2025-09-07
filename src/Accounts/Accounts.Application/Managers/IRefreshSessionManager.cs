using Accounts.Domain;
using SharedKernel.Shared;

namespace Accounts.Application.Managers;

public interface IRefreshSessionManager
{
    Task Delete(RefreshSession refreshSession, CancellationToken cancellationToken = default);
    Task<Result<RefreshSession>> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken = default);
}