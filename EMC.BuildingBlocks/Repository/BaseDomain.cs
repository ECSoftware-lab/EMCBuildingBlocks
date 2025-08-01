namespace EMC.BuildingBlocks.Repository
{
    public abstract class BaseDomain
    {
       // public Guid Id { get; set; }  saque esto 
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? ModifieBy { get; set; }
        public DateTime? ModifieDate { get; set; }
        public bool Status { get; set; } = true;
    }
}
