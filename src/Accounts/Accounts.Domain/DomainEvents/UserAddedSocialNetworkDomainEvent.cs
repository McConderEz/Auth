using MediatR;

namespace Accounts.Domain.DomainEvents;

public record UserAddedSocialNetworkDomainEvent(Guid UserId) : INotification;
