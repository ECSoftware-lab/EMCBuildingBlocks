namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.Turnos
{
    /// <summary>
    /// Turno cancelado — para notificar al cliente que su turno fue cancelado.
    /// Escucha: EMC.Worker → TurnoCanceladoConsumer → email/WhatsApp de cancelación.
    /// </summary>
    public class TurnoCanceladoIntegrationEvent : IntegrationEvent
    {
        public Guid TurnoId { get; set; }
        public string ClienteDisplayName { get; set; } = null!;
        public string ClienteEmail { get; set; } = null!;
        public string? ClienteTelefono { get; set; }
        public string PracticaNombre { get; set; } = null!;
        public DateTimeOffset FechaHoraInicio { get; set; }
        public string? Motivo { get; set; }
    }
}
