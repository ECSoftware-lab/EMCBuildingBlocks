using EMC.BuildingBlocks.EventBus;

namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.UserEvents
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public string Email { get; set; }
        public string FullName { get; set; } // opcional, si lo tenés
        public string ConfirmationLink { get; set; }
        public string Template { get; set; } // puede ser el HTML o el nombre de plantilla
    }

}
