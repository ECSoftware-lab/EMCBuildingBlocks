namespace EMC.BuildingBlocks.Wrappers
{
    public interface IApiResponse<T> : ICommonResponse
    {

        T Response { get; set; }
    }
}