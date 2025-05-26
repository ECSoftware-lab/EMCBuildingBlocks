namespace EMC.BuildingBlocks.Dtos
{
    public class AddressGenericDto
    {
        public string AddressId { get; set; }
        public int City { get; set; }
        public string Floor { get; set; }
        public string FloorLetter { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Zipcode { get; set; }
        public string Complement { get; set; }
        public bool IsCreate { get; set; }
        public bool IsFavorit { get; set; }
    }
}
