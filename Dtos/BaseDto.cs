namespace EMC.BuildingBlocks.Dtos
{
    public class BaseDto
    { 
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool Status { get; set; }
        
    }
}
