using Newtonsoft.Json;

namespace EMC.BuildingBlocks.Dtos.AddresDtos
{
    public class CityResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

    }
}
