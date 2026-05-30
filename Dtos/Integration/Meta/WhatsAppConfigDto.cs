namespace EMC.BuildingBlocks.Dtos.Integration.Meta
{
    public class WhatsAppConfigDto
    {
        public int Id { get; set; }
        public Guid CompanyId { get; set; } // multi-tenant
        public Guid SubsidiaryId { get; set; } // multi-tenant
        public string PhoneNumberId { get; set; } = "";     // ID del número en Meta
        public string AccessToken { get; set; } = "";     // cifrado en BD
        public string BusinessId { get; set; } = "";
        public DateTime TokenExpiresAt { get; set; }
        public bool Active { get; set; } = true;
        public string Ambiente { get; set; } = "";     // "dev" | "prod"
                                                       // en WhatsAppConfig agregar
        public string? DisplayPhoneNumber { get; set; }
        public string? VerifiedName { get; set; }
        public string WabaId { get; set; }

        public string WabaName { get; set; }
        public BaseDto? Audit { get; set; } = new BaseDto();
    }

}
