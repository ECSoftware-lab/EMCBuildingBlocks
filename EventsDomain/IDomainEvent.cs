using MediatR;

namespace EMC.BuildingBlocks.EventsDomain
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
