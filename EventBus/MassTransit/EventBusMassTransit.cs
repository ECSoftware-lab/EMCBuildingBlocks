using MassTransit;

namespace EMC.BuildingBlocks.EventBus.MassTransit
{
    public class EventBusMassTransit : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBusMassTransit(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            await _publishEndpoint.Publish(@event);
        }
    }


    public sealed class NoOpEventBus : IEventBus
    {
        public Task PublishAsync<T>(T @event) where T : IntegrationEvent
            => Task.CompletedTask; // no hace nada
    }
}
