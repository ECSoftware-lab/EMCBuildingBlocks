using EMC.BuildingBlocks.Application;
using EMC.BuildingBlocks.Wrappers;
using FluentValidation.Results;
using System.Net;

namespace EMC.BuildingBlocks.Exceptions
{
    public class ValidationErrorException : Exception, ICommonResponse
    {
        public HttpStatusCode Status { get; set; }
        public List<ApiError> Errors { get; set; }
        public bool IsSuccess => false; // Siempre falso para excepciones
        public string Message { get; set; }
        public List<string> Remarks { get; set; }
        public DateTime Timestamp { get; set; }
        public object Response { get; set; } = null;
        public ValidationErrorException() : base("Se presentaron uno o mas errores de validacion")
        {
            Status = HttpStatusCode.BadRequest;
            Timestamp = DateTime.UtcNow;
            Message = "Se presentaron uno o mas errores de validacion";
            Errors = new List<ApiError>();
            Remarks = new List<string>();
        }

        public ValidationErrorException(IEnumerable<ValidationFailure> failures) : this()
        {
            //foreach (var failure in failures)
            //{
            //    Remarks.Add(failure.ErrorMessage);
            //    Errors.Add(ErrorCodes.GetCode(ErrorCodes.Codes.FieldNotValid).Clone(failure.ErrorMessage));
            //}
            foreach (var failure in failures.DistinctBy(f => f.ErrorMessage))
            {
                Remarks.Add(failure.ErrorMessage);
                Errors.Add(ErrorCodes.GetCode(ErrorCodes.Codes.FieldNotValid).Clone(failure.ErrorMessage));
            }

        }


    }
}
