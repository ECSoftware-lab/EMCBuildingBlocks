namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.Productos
{
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    {
        public ProductEventDto ProductEvent { get; set; }

        public ProductCreatedIntegrationEvent() { }

        public ProductCreatedIntegrationEvent(ProductEventDto productEvent)
        {
            ProductEvent = productEvent;
        }
    }

}
