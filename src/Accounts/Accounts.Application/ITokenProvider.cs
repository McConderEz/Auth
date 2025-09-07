using System.Security.Claims;
using Accounts.Application.Models;
using Accounts.Domain;
using SharedKernel.Shared;

namespace Accounts.Application;

public interface ITokenProvider
{
    Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken cancellationToken = default);
    Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<Claim>>> GetUserClaimsFromJwtToken(string jwtToken, CancellationToken cancellationToken = default);
}