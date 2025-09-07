using SharedKernel.Shared;

namespace Accounts.Domain.DomainEvents;

public record UserAddedAvatarDomainEvent(Guid UserId) : IDomainEvent;
