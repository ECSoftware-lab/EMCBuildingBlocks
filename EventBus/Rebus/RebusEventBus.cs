using Rebus.Bus;

namespace EMC.BuildingBlocks.EventBus.Rebus
{
    public class RebusEventBus : IEventBus
    {
        private readonly IBus _bus;

        public RebusEventBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            await _bus.Publish(@event);
        }

    }

    public sealed class NoOpEventBus : IEventBus
    {
        public Task PublishAsync<T>(T @event) where T : IntegrationEvent
            => Task.CompletedTask; // no hace nada
    }
}
