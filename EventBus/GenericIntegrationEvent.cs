using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMC.BuildingBlocks.EventBus
{
    /// <summary>
    /// Envelope genérico para publicar cualquier evento del Outbox al bus.
    /// Los consumidores usan EventType para enrutar.
    /// </summary>
    public class GenericIntegrationEvent : IntegrationEvent
    {
        public string EventType { get; }
        public string Payload { get; }

        public GenericIntegrationEvent(string eventType, string payload)
        {
            EventType = eventType;
            Payload = payload;
        }
    }
}
