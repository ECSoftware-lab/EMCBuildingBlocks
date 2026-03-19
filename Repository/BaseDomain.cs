namespace EMC.BuildingBlocks.Repository
{
    public abstract class BaseDomain
    { 
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Status { get; set; } = true;
    }
}
