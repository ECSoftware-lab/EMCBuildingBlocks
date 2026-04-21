namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.Turnos
{
    /// <summary>
    /// Turno creado — para notificar al cliente que su reserva está registrada.
    /// Escucha: EMC.Worker → TurnoCreadoConsumer → email/WhatsApp de confirmación de reserva.
    /// </summary>
    public class TurnoCreadoIntegrationEvent : IntegrationEvent
    {
        public Guid TurnoId { get; set; }
        public string ClienteDisplayName { get; set; } = null!;
        public string ClienteEmail { get; set; } = null!;
        public string? ClienteTelefono { get; set; }
        public string PracticaNombre { get; set; } = null!;
        public string SucursalNombre { get; set; } = null!;
        public DateTimeOffset FechaHoraInicio { get; set; }
        public DateTimeOffset FechaHoraFin { get; set; }
    }
}
