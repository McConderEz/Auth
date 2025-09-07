using SharedKernel.Shared;

namespace Accounts.Domain.DomainEvents;

public record UserInfoUpdatedDomainEvent(Guid UserId) : IDomainEvent;
