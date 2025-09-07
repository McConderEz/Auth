using Accounts.Application.Managers;
using Accounts.Domain;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Constraints;
using SharedKernel.Shared;
using SharedKernel.Shared.Errors;

namespace Accounts.Infrastructure.IdentityManagers;

public class PermissionManager(AccountsDbContext accountsDbContext,
    [FromKeyedServices(Constraints.Context.Accounts)] IUnitOfWork unitOfWork) : IPermissionManager
{
    public async Task<Permission?> FindByCode(string code)
    {
        return await accountsDbContext.Permissions.FirstOrDefaultAsync(p => p.Code == code);
    }
    
    public async Task AddRangeIfExist(IEnumerable<string> permissions)
    {
        foreach (var permissionCode in permissions)
        {
            var isPermissionExist = await accountsDbContext.Permissions
                .AnyAsync(p => p.Code == permissionCode);
        
            if(isPermissionExist)
                continue;

            await accountsDbContext.Permissions.AddAsync(new Permission { Code = permissionCode });
        }
        
        await unitOfWork.SaveChanges();
    }

    public async Task<Result<List<string>>> GetPermissionsByUserId(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await accountsDbContext.Users
            .Include(u => u.Roles)
                .ThenInclude(r => r.RolePermissions)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if (user is null)
            return Errors.General.NotFound();

        var permissions = user.Roles
            .SelectMany(r => r.RolePermissions.Select(rp => rp.Permission.Code)).ToList();
        
        return permissions;
    }
}