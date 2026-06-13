using EMC.BuildingBlocks.Http;
using EMC.ErrorHandling.Models;

namespace EMC.BuildingBlocks.Static.Extensions
{
    public static class ExternalApitExtensions
    {
        public static List<ApiError> GetExternalApiErrors(this List<ExternalApiError> errors)
            => errors.Select(e => new ApiError { Code = e.Code, Message = e.Message }).ToList();
    }
}
