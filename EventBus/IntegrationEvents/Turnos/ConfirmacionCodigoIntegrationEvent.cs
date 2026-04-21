namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.Turnos
{
    /// <summary>
    /// El turno requiere confirmación por código.
    /// Escucha: EMC.Worker → ConfirmacionCodigoConsumer → email/WhatsApp con el código.
    /// </summary>
    public class ConfirmacionCodigoIntegrationEvent : IntegrationEvent
    {
        public Guid TurnoId { get; set; }
        public string ClienteDisplayName { get; set; } = null!;
        public string ClienteEmail { get; set; } = null!;
        public string? ClienteTelefono { get; set; }
        public string PracticaNombre { get; set; } = null!;
        public DateTimeOffset FechaHoraInicio { get; set; }
        public string Codigo { get; set; } = null!;
        public DateTimeOffset Expira { get; set; }

        /// <summary>
        /// Link para cancelar el turno directamente desde el email/WhatsApp.
        /// Ejemplo: https://tuapp.com/turnos/{TurnoId}/cancelar?token={token}
        /// </summary>
        public string LinkCancelar { get; set; } = null!;
    }
}
