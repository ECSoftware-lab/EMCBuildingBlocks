namespace EMC.BuildingBlocks.Dtos
{
    public class BaseDto
    { 
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; } 
        public int? ModifieBy { get; set; }
        public DateTime? ModifieDate { get; set; }
        public int? DeleteBy { get; set; }
    }
}
