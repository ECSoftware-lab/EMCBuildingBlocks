using EMC.BuildingBlocks.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMC.BuildingBlocks.Exceptions
{
   
   public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerOptions _jsonOptions;

        public ErrorHandlingMiddleware(RequestDelegate next, IOptions<JsonOptions> jsonOptions)
        {
            _next = next;
            // reutiliza EXACTAMENTE las opciones configuradas en AddJsonOptions (camelCase)
            _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await WriteProblemAsync(context, ex);
            }
        }

        private async Task WriteProblemAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            if (ex is ICommonResponse cr)
            {
                // mapear tu contrato a una forma neutral y camelCase
                var payload = new
                {
                    status = (int)cr.Status,
                    isSuccess = false,
                    message = cr.Message,
                    errors = cr.Errors?.Select(e => new { code = e.Code, message = e.Message }) ?? Enumerable.Empty<object>(),
                    remarks = cr.Remarks ?? new List<string>(),
                    response = (object?)null,
                    timestamp = cr.Timestamp
                };

                context.Response.StatusCode = (int)cr.Status;
                await context.Response.WriteAsync(JsonSerializer.Serialize(payload, _jsonOptions));
                return;
            }

            // fallback 500
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var generic = new
            {
                status = 500,
                isSuccess = false,
                message = "Ocurrió un error inesperado.",
                errors = new[] { new { code = 0, message = ex.Message } },
                remarks = Array.Empty<string>(),
                response = (object?)null,
                timestamp = DateTime.UtcNow
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(generic, _jsonOptions));
        }
    }
}
