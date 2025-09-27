using Newtonsoft.Json;

namespace EMC.BuildingBlocks.Dtos.AddresDtos
{
    public class CountryResponse : TResponse
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("iso2")]
        public string? Iso2 { get; set; }


    }
}
