using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMC.BuildingBlocks.Http
{
    /// <summary>
    /// Envelope de respuesta común a todos los microservicios EMC.
    /// Todos devuelven HTTP 200 siempre. El resultado real está en IsSuccess + Status interno.
    /// Este record va en BuildingBlocks para que cualquier MS que consuma a otro lo use.
    /// </summary>
    public record ExternalApiResponse<T>
    {
        public int Status { get; init; }
        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;
        public List<ExternalApiError> Errors { get; init; } = [];
        public List<string> Remarks { get; init; } = [];
        public T? Response { get; init; }
        public DateTimeOffset Timestamp { get; init; }

        public bool HasResponse => IsSuccess && Response is not null;
    }

    public record ExternalApiError(int Code, string Message);
}
