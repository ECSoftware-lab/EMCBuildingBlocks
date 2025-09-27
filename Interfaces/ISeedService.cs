using EMC.BuildingBlocks.Dtos;

namespace EMC.BuildingBlocks.Interfaces
{
    public interface ISeedService
    {
        Task<TResponse> GetListAsync<T>(string servicePrefix, string controller);

    }
}
