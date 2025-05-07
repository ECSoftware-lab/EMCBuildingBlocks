namespace EMC.BuildingBlocks.Repository
{
    public abstract class BaseDomainModel : BaseDomain
    {
        public int? ModifieBy { get; set; }
        public DateTime? ModifieDate { get; set; }
        public int? DeleteBy { get; set; }
    }
}
