using Microsoft.Extensions.Logging;

namespace EMC.BuildingBlocks.Application
{

    public class ApiError : IEquatable<ApiError>
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ApiError Clone()
        {
            return new ApiError
            {
                Code = Code,
                Message = Message
            };
        }
        public ApiError Clone(string mensj)
        {
            var toReturn = Clone();
            toReturn.Message = toReturn.Message + " " + mensj;

            return toReturn;
        }
        public bool Equals(ApiError toCompare) => Code == (toCompare?.Code ?? 0);

        public static void AddError(List<ApiError> errorsList, int errorCode, string additionalMessage = "")
        {
            var error = ErrorCodes.Messages.FirstOrDefault(e => e.Code == errorCode)?.Clone(additionalMessage);
            if (error != null)
            {
                errorsList.Add(error);
            }
        }
        public static EventId GetError(int errorCode, string additionalMessage = "")
        {
            var error = ErrorCodes.Messages.FirstOrDefault(e => e.Code == errorCode)?.Clone(additionalMessage);
            EventId eventId = new EventId(error.Code, error.Message);
            return eventId;
        }
    }
}
