using MediatR;
using SharedKernel.Shared.Ids;
using SharedKernel.Shared.Objects;

namespace SharedKernel.Shared;

public static class MediatrExtension
{
    public static async Task PublishDomainEvents<TId>(
        this IPublisher publisher,
        DomainEntity<TId> entity,
        CancellationToken cancellationToken = default) where TId: BaseId<TId>
    {
        foreach (var @event in entity.DomainEvents)
        {
            await publisher.Publish(@event, cancellationToken);
        }
        
        entity.ClearDomainEvents();
    } 
    
    public static async Task  PublishDomainEvent(
        this IPublisher publisher,
        IDomainEvent @event,
        CancellationToken cancellationToken = default)
    {
        await publisher.Publish(@event, cancellationToken);
    }
}