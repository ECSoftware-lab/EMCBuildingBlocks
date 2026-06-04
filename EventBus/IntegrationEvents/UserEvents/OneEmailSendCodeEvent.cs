namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.UserEvents
{
    public class OneEmailSendCodeEvent : IntegrationEvent
    {
        public string Email { get; set; }
        public string DisplayName { get; set; } // opcional, si lo tenés
        public string ConfirmationLink { get; set; }
        public string Template { get; set; } // puede ser el HTML o el nombre de plantilla
        public int ExpiresIn { get; set; }
    }

}
