namespace EMC.BuildingBlocks.EventBus
{
    public abstract class IntegrationEvent
    {
        public Guid IdEvent { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public Guid CompanyId { get; set; }
        protected IntegrationEvent()
        {//pregunta en que momento se llama a este metodo ? 
            IdEvent = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }       
       
    }
}
