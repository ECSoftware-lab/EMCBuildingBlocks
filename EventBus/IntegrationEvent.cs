namespace EMC.BuildingBlocks.EventBus
{
    public abstract class IntegrationEvent
    {
        public Guid IdEvent { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public Guid CompanyId { get; set; }
        protected IntegrationEvent()
        { 
            IdEvent = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }       
       
    }
}
