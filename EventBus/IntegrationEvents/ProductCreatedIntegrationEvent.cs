namespace EMC.BuildingBlocks.EventBus.IntegrationEvents
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
