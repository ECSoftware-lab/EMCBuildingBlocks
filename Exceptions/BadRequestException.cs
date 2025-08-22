using EMC.BuildingBlocks.Application;
using EMC.BuildingBlocks.Wrappers;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EMC.BuildingBlocks.Exceptions
{
    public class BadRequestException : Exception, ICommonResponse
    {
        public HttpStatusCode Status { get; set; }
        public List<ApiError> Errors { get; set; }
        public bool IsSuccess => false;
        public string Message { get; set; }
        public List<string> Remarks { get; set; }
        public DateTime Timestamp { get; set; }
        public object Response { get; set; } = null;
        public BadRequestException(string message) : base(message)
        {
            Status = HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = message;
            Errors = new List<ApiError>();
            Remarks = new List<string>();
        }
        public BadRequestException(string message, Exception ex, HttpStatusCode? statusCode = null)
    : base(message, ex)
        {
            Status = statusCode ?? HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = message;
            Errors = new List<ApiError>();

            Remarks = new List<string>();
            if (ex?.InnerException != null)
            {
                Errors.Add(new ApiError
                {
                    Code = 805,
                    Message = ex.InnerException.Message
                });
            }
            if (ex != null)
            {
                Remarks.Add($"Inner: {ex.Message}");
                // O más detallado si lo necesitás:
                // Remarks.Add($"StackTrace: {ex.StackTrace}");
            }
        }

        public BadRequestException(string message, List<ApiError> errors, HttpStatusCode? statusCode = null) : base(message)
        {
            Status = statusCode ?? HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = message;
            Errors = errors ?? new List<ApiError>();
            Remarks = new List<string>();
        }
        public BadRequestException(ILogger logger, string message, List<ApiError> errors, int level) : base(message)
        {
            Status = HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = message;
            Errors = errors ?? new List<ApiError>();
            Remarks = new List<string>();
            if (Status == HttpStatusCode.BadRequest)
            {
                if (Errors != null && Errors.Count > 0 && level > 0)
                {
                    foreach (var error in Errors)
                    {
                        EventId eventId = ApiError.GetError(error.Code, error.Message);
                        switch (level)
                        {
                            case 6:
                                logger.LogCritical(eventId, message);
                                break;

                            case 5:
                                logger.LogError(eventId, message);
                                break;

                            case 4:
                                logger.LogWarning(eventId, message);
                                break;

                            default:
                                // Manejo para niveles no contemplados (opcional)
                                logger.LogInformation(eventId, $"Unhandled level: {level}, {message}");
                                break;
                        }
                    }


                }
            }



        }

    }
    public class ExceptionCompany : Exception, ICommonResponse
    {
        public HttpStatusCode Status { get; set; }
        public List<ApiError> Errors { get; set; }
        public bool IsSuccess => false; // Siempre falso para excepciones
        public string Message { get; set; }
        public List<string> Remarks { get; set; }
        public DateTime Timestamp { get; set; }
        public object Response { get; set; } = null;
        public ExceptionCompany(string message) : base(message)
        {
            Status = HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = $"En Empresa {message}";
            Errors = new List<ApiError>();
            Remarks = new List<string>();
        }
        public ExceptionCompany(string message, List<ApiError> errors, HttpStatusCode? statusCode = null) : base(message)
        {
            Status = statusCode ?? HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = $"En Empresa {message}";
            Errors = errors ?? new List<ApiError>();
            Remarks = new List<string>();
        }

    }

    public class ArgNullException : Exception, ICommonResponse
    {
        public HttpStatusCode Status { get; set; }
        public List<ApiError> Errors { get; set; }
        public bool IsSuccess => false; // Siempre falso para excepciones
        public string Message { get; set; }
        public List<string> Remarks { get; set; }
        public DateTime Timestamp { get; set; }
        public object Response { get; set; } = null;
        public ArgNullException(string message) : base(message)
        {
            Status = HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = $"Null {message}";
            Errors = new List<ApiError>();
            Remarks = new List<string>();
        }
        public ArgNullException(string message, List<ApiError> errors, HttpStatusCode? statusCode = null) : base(message)
        {
            Status = statusCode ?? HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = $"Null {message}";
            Errors = errors ?? new List<ApiError>();
            Remarks = new List<string>();
        }

    }

    public class BadRequestUserException : Exception, ICommonResponse
    {
        public HttpStatusCode Status { get; set; }
        public List<ApiError> Errors { get; set; }
        public bool IsSuccess => false; // Siempre falso para excepciones
        public string Message { get; set; }
        public List<string> Remarks { get; set; }
        public DateTime Timestamp { get; set; }
        public object Response { get; set; } = null;
        public BadRequestUserException(string message) : base(message)
        {
            Status = HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = $"{message}";
            Errors = new List<ApiError>();
            Remarks = new List<string>();
        }
        public BadRequestUserException(string message, List<ApiError> errors, HttpStatusCode? statusCode = null) : base(message)
        {
            Status = statusCode ?? HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = $"{message}";
            Errors = errors ?? new List<ApiError>();
            Remarks = new List<string>();
        }

    }


    public class NotFoundException : Exception, ICommonResponse
    {
        public HttpStatusCode Status { get; set; }
        public List<ApiError> Errors { get; set; }
        public bool IsSuccess => false; // Siempre falso para excepciones
        public string Message { get; set; }
        public List<string> Remarks { get; set; }
        public DateTime Timestamp { get; set; }
        public object Response { get; set; } = null;
        public NotFoundException(string message) : base(message)
        {
            Status = HttpStatusCode.NotFound;
            Timestamp = DateTime.UtcNow;
            Message = $"{message}";
            Errors = new List<ApiError>();
            Remarks = new List<string>();
        }
        public NotFoundException(string message, List<ApiError> errors, List<string> remark) : base(message)
        {
            Status = HttpStatusCode.NotFound;
            Timestamp = DateTime.UtcNow;
            Message = $"{message}";
            Errors = errors ?? new List<ApiError>();
            Remarks = remark ?? new List<string>();
        }

    }
    public class ForbiddenAccessException : Exception, ICommonResponse
    {
        public HttpStatusCode Status { get; set; }
        public List<ApiError> Errors { get; set; }
        public bool IsSuccess => false; // Siempre falso para excepciones
        public string Message { get; set; }
        public List<string> Remarks { get; set; }
        public DateTime Timestamp { get; set; }
        public object Response { get; set; } = null;
        public ForbiddenAccessException(string message) : base(message)
        {
            Status = HttpStatusCode.Forbidden;
            Timestamp = DateTime.UtcNow;
            Message = $"{message}";
            Errors = new List<ApiError>();
            Remarks = new List<string>();
        }


    }
    public class ProductDomainException : Exception, ICommonResponse
    {
        public HttpStatusCode Status { get; set; }
        public List<ApiError> Errors { get; set; }
        public bool IsSuccess => false;
        public string Message { get; set; }
        public List<string> Remarks { get; set; }
        public DateTime Timestamp { get; set; }
        public object Response { get; set; } = null; 
        public ProductDomainException(ILogger logger, string message, List<ApiError> errors, int level) : base(message)
        {
            Status = HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = message;
            Errors = errors ?? new List<ApiError>();
            Remarks = new List<string>();
            if (Status == HttpStatusCode.BadRequest)
            {
                if (Errors != null && Errors.Count > 0 && level > 0)
                {
                    foreach (var error in Errors)
                    {
                        EventId eventId = ApiError.GetError(error.Code, error.Message);
                        switch (level)
                        {
                            case 6:
                                logger.LogCritical(eventId, message);
                                break;

                            case 5:
                                logger.LogError(eventId, message);
                                break;

                            case 4:
                                logger.LogWarning(eventId, message);
                                break;

                            default:
                                // Manejo para niveles no contemplados (opcional)
                                logger.LogInformation(eventId, $"Unhandled level: {level}, {message}");
                                break;
                        }
                    }


                }
            }



        }
        public ProductDomainException(string message) : base(message)
        {
            Status = HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = message;
            Errors = new List<ApiError>();
            Remarks = new List<string>();
        }

    }


}
