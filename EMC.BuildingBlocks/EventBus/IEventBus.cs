namespace EMC.BuildingBlocks.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : IntegrationEvent;
    }
}
