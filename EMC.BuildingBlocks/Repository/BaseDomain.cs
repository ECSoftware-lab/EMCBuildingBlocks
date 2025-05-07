namespace EMC.BuildingBlocks.Repository
{
    public abstract class BaseDomain
    { 
        public int Id { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; } = true;
    }
}
