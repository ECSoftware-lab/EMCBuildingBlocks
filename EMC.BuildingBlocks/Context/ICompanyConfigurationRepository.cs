namespace EMC.BuildingBlocks.Context
{
    public interface ICompanyConfigurationRepository
    {
        Task<Dictionary<string, string>> GetAllByCompanyAsync(int companyId);

    }

}
