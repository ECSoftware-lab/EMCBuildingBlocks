namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.CompanyEvents
{
    public class CompanyRegisteredIntegrationEvent : IntegrationEvent
    {
        public string Email { get; set; }
        public string FullName { get; set; } // opcional, si lo tenés
        public string ConfirmationCode { get; set; }
        public string Template { get; set; } // puede ser el HTML o el nombre de plantilla
        public string Phone { get; set; }
        public string SubDomain { get; set; }
        public string UrlFront { get; set; }
    }

}
