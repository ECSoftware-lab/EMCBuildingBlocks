namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.UserEvents
{
    public class OneSendCodeEvent : IntegrationEvent
    {
        // destino
        public string PhoneDestino { get; set; } = "";  // "5493811234567" — formato E.164 sin +         
        public string Code { get; set; } = "";
        public int ExpiresIn { get; set; }           // minutos
        // contexto del usuario — para el mensaje
        public string DisplayName { get; set; } = "";

        public string Type { get; set; } = "validate"; // "login" | "verification" | "2fa" — para el mensaje
        public string PhoneCompany { get; set; } = "";
        public string TypeMenssage { get; set; } = "whatsapp";// "sms" | "whatsapp" — para el mensaje
    }
}
