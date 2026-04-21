namespace EMC.BuildingBlocks.EventBus.IntegrationEvents.Turnos
{/// <summary>
 /// El turno requiere pago de seña para confirmarse.
 /// Escucha: EMC.Worker → TurnoPendientePagoConsumer → email/WhatsApp con link de pago.
 /// El link apunta a tu app, que en el momento del click genera el initPoint de MP.
 /// </summary>
    public class TurnoPendientePagoIntegrationEvent : IntegrationEvent
    {
        public Guid TurnoId { get; set; }
        public string ClienteDisplayName { get; set; } = null!;
        public string ClienteEmail { get; set; } = null!;
        public string? ClienteTelefono { get; set; }
        public string PracticaNombre { get; set; } = null!;
        public DateTimeOffset FechaHoraInicio { get; set; }
        public decimal MontoSeña { get; set; }

        /// <summary>
        /// Link a tu app. Al clickear, tu endpoint llama a MP y redirige al checkout.
        /// Ejemplo: https://tuapp.com/turnos/{TurnoId}/pagar
        /// </summary>
        public string LinkPago { get; set; } = null!;
    }
}
