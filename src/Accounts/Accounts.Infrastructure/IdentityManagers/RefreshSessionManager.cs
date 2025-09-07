using Accounts.Application.Managers;
using Accounts.Domain;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedKernel.Constraints;
using SharedKernel.Shared;
using SharedKernel.Shared.Errors;

namespace Accounts.Infrastructure.IdentityManagers;

public class RefreshSessionManager(
    AccountsDbContext accountsDbContext,
    [FromKeyedServices(Constraints.Context.Accounts)] IUnitOfWork unitOfWork,
    ILogger<RefreshSessionManager> logger) : IRefreshSessionManager
{

    public async Task<Result<RefreshSession>> GetByRefreshToken(
        Guid refreshToken, CancellationToken cancellationToken = default)
    {
        var refreshSession = await accountsDbContext.RefreshSessions
            .Include(r => r.User)
                .ThenInclude(u => u.ParticipantAccount)
            .Include(r => r.User)
                .ThenInclude(u => u.Roles)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.RefreshToken == refreshToken, cancellationToken);

        if (refreshSession is null)
            return Errors.General.NotFound();

        return refreshSession;
    }
    
    public async Task Delete(RefreshSession refreshSession, CancellationToken cancellationToken = default)
    {
        try
        {
            var existingSession = await accountsDbContext.RefreshSessions
                .FirstOrDefaultAsync(r => r.RefreshToken == refreshSession.RefreshToken, cancellationToken);
            
            if (existingSession != null)
            {
                accountsDbContext.RefreshSessions.Remove(existingSession);
                await unitOfWork.SaveChanges(cancellationToken);
            }
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogError("RefreshSession already deleted or modified: " + ex.Message);
        }
    }
}