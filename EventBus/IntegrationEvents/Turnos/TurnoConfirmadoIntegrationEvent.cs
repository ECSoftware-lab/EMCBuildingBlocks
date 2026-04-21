namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.Turnos
{
    /// <summary>
    /// Turno confirmado — para notificar que el turno está confirmado definitivamente.
    /// Escucha: EMC.Worker → TurnoConfirmadoConsumer → email/WhatsApp de confirmación.
    /// </summary>
    public class TurnoConfirmadoIntegrationEvent : IntegrationEvent
    {
        public Guid TurnoId { get; set; }
        public string ClienteDisplayName { get; set; } = null!;
        public string ClienteEmail { get; set; } = null!;
        public string? ClienteTelefono { get; set; }
        public string PracticaNombre { get; set; } = null!;
        public string SucursalNombre { get; set; } = null!;
        public DateTimeOffset FechaHoraInicio { get; set; }
    }
}
