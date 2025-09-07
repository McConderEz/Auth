using SharedKernel.Shared;
using SharedKernel.Shared.Errors;
using Microsoft.AspNetCore.Identity;

namespace Accounts.Application.Extensions;

public static class IdentityExtensions
{
    public static ErrorList ToErrorList(this IEnumerable<IdentityError> identityErrors)
    {
        var errors = identityErrors.Select(ie => Error.Failure(ie.Code, ie.Description));
        
        return new ErrorList(errors);
    }
}