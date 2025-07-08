namespace EMC.BuildingBlocks.Dtos
{
    public class AddressAppModel
    {
        public string AddressId { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string Floor { get; set; }
        public string FloorLetter { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string Complement { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        
        public bool IsCreate { get; set; }
        public bool IsFavorit { get; set; }
        public bool Status { get; set; }
    }
}
