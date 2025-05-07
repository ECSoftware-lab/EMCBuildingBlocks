using EMC.BuildingBlocks.Application;
using System.Net;

namespace EMC.BuildingBlocks.Wrappers
{
    public interface ICommonResponse
    {
        HttpStatusCode Status { get; set; }
        List<ApiError> Errors { get; set; }
        bool IsSuccess { get; }
        string Message { get; set; }
        List<string> Remarks { get; set; }
        DateTime Timestamp { get; set; }
    }
}
