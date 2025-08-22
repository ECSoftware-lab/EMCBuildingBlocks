using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EMC.BuildingBlocks.Exceptions
{
    public class CustomValidationFilter : IActionFilter
    {
        private readonly ILogger<CustomValidationFilter> _logger;

        public CustomValidationFilter(ILogger<CustomValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var failures = context.ModelState
                    .SelectMany(ms => ms.Value.Errors)
                    .Select(e => new ValidationFailure(e.ErrorMessage, e.ErrorMessage))
                    .ToList();

                _logger.LogWarning("Validation errors in filter: {Errors}",
                    string.Join(", ", failures.Select(f => f.ErrorMessage)));

                throw new ValidationErrorException(failures);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
