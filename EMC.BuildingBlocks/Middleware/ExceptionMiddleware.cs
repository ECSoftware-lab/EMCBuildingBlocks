using EMC.BuildingBlocks.Application;
using EMC.BuildingBlocks.Exceptions;
using EMC.BuildingBlocks.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace EMC.BuildingBlocks.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {


            try
            {
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    List<EventId> eventIds = new List<EventId>();
                    eventIds.Add(new EventId(StatusCodes.Status429TooManyRequests, "demasiadas solicitudes"));
                    var toResonse =
                         ApiResponse<string>.Failure("Has hecho demasiadas solicitudes. Intenta nuevamente más tarde"
                         , eventIds, HttpStatusCode.BadRequest);

                    await ModifyHeader(context, toResonse, StatusCodes.Status429TooManyRequests);
                }
            }
            catch (Exception ex)
            {
                if (ex is TaskCanceledException || ex is TimeoutException)
                {
                    List<EventId> eventIds = new List<EventId>();
                    eventIds.Add(new EventId(StatusCodes.Status408RequestTimeout, "Solicitud agotada debe intentar luego"));
                    var toResonse =
                         ApiResponse<string>.Failure("Solicitud agotada"
                         , eventIds, HttpStatusCode.BadRequest);

                    await ModifyHeader(context, toResonse, StatusCodes.Status429TooManyRequests);
                }
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                var result = ex switch
                {
                    ValidationErrorException validationEx =>
                        ApiResponse<string>.Failure(validationEx.Message, validationEx.Errors, HttpStatusCode.BadRequest),

                    BadRequestException badRequestEx =>
                        ApiResponse<string>.Failure(badRequestEx.Message, badRequestEx.Errors, HttpStatusCode.BadRequest),

                    ArgNullException NullRequestEx =>
                        ApiResponse<string>.Failure(NullRequestEx.Message, NullRequestEx.Errors, HttpStatusCode.BadRequest),

                    ExceptionCompany companyEx =>
                                companyEx.Status == HttpStatusCode.NotFound
                                ? ApiResponse<string>.Empty(companyEx.Status, companyEx.Message)
                                : ApiResponse<string>.Failure(companyEx.Message, companyEx.Errors, companyEx.Status),

                    BadRequestUserException UserRequestEx =>
                        UserRequestEx.Status == HttpStatusCode.NotFound
                                ? ApiResponse<string>.Empty(UserRequestEx.Status, UserRequestEx.Message)
                    : ApiResponse<string>.Failure(UserRequestEx.Message, UserRequestEx.Errors, UserRequestEx.Status),

                    NotFoundException notFoundEx =>
                    ApiResponse<string>.Empty(HttpStatusCode.NotFound, notFoundEx.Message),

                    UnauthorizedAccessException _ =>
                     ApiResponse<string>.Empty(HttpStatusCode.Unauthorized, "No autorizado"),

                    ForbiddenAccessException forbiddenEx =>
         ApiResponse<string>.Empty(HttpStatusCode.Forbidden, forbiddenEx.Message),


                    _ => ApiResponse<string>.Failure("Ocurrió un error inesperado", new List<ApiError>
            {
                new ApiError { Code = 500, Message = ex.Message }
            }, HttpStatusCode.InternalServerError)
                };

                context.Response.StatusCode = (int)result.Status;
                var jsonResponse = JsonConvert.SerializeObject(result);
                await context.Response.WriteAsync(jsonResponse);
            }

        }

        private static async Task ModifyHeader(HttpContext context, ApiResponse<string> apiResponse, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse), CancellationToken.None);

            return;
        }
    }
}
/* case FluentValidation.ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var validationJson2 = JsonConvert.SerializeObject(validationException.Errors);
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, validationJson2));
                        break;*/