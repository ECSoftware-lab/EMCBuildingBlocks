namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.UserEvents
{
    public class UserRecoverPasswordIntegrationEvent : IntegrationEvent
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Link { get; set; }
        public string Template { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }

}
