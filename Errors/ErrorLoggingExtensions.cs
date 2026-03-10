using Microsoft.Extensions.Logging;

namespace EMC.BuildingBlocks.Errors
{
    public static class ErrorLoggingExtensions
    {
        public static EventId ToEventId(this ErrorDefinition error)
        {
            return new EventId(error.Code, error.Message);
        }
        public static EventId ToEventId(this ErrorResult error)
        {
            return new EventId(error.Code, error.Message);
        }
    }
}
