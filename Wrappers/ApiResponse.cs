using EMC.BuildingBlocks.Application;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EMC.BuildingBlocks.Wrappers
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public bool IsSuccess => Status == HttpStatusCode.OK ? true : false;

        public string Message { get; set; }
        public List<ApiError> Errors { get; set; }
        public List<string> Remarks { get; set; }
        public T Response { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public ApiResponse()
        {

        }
        private ApiResponse(T result, List<string>? remarks, string? message)
        {
            Response = result;
            Status = HttpStatusCode.OK;
            Remarks = remarks == null ? new List<string>() : remarks;
            Errors = new List<ApiError>();
            Message = message ?? string.Empty;
        }
        public static ApiResponse<T> Success(T result, List<string>? remarks, string? title = null) => new ApiResponse<T>(result, remarks, title);
        private ApiResponse(string message, List<ApiError> errors, HttpStatusCode? statu = HttpStatusCode.BadRequest)
        {
            Errors = errors == null ? new List<ApiError>() : errors;
            Remarks = new List<string>();
            Status = statu == null ? HttpStatusCode.BadRequest : statu.Value;
            Message = string.IsNullOrEmpty(message) ? "Ocurrio un error inesperado" : message;
        }

        private ApiResponse(string message, List<EventId> errors, HttpStatusCode? statu = HttpStatusCode.BadRequest)
        {
            EventId eventId = new EventId();

            Errors = new List<ApiError>();
            if (errors != null && errors.Count > 0)
            {
                foreach (var item in errors)
                {
                    var code = item.Id;
                    var mens = item.Name;
                    Errors.Add(new ApiError { Code = code, Message = mens });
                }
            }

            Remarks = new List<string>();
            Status = statu == null ? HttpStatusCode.BadRequest : statu.Value;
            Message = string.IsNullOrEmpty(message) ? "Ocurrio un error inesperado" : message;
        }
        public static ApiResponse<T> Failure(string title,
            List<ApiError> errors,  HttpStatusCode? statu = HttpStatusCode.BadRequest)
            => new ApiResponse<T>(title, errors, statu);


        public static ApiResponse<T> Failure(string title,
           List<EventId> errors, HttpStatusCode? statu = HttpStatusCode.BadRequest)
           => new ApiResponse<T>(title, errors, statu);
        public static ApiResponse<T> Empty(HttpStatusCode status, string? message = null)
        {
            return new ApiResponse<T>
            {
                Status = status,
                Message = message ?? string.Empty,
                Errors = new List<ApiError>(),
                Remarks = new List<string>(),
                Response = default
            };
        }


    }
}
