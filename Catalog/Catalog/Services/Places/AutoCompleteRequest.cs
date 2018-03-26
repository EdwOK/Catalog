using Newtonsoft.Json;

namespace Catalog.Services.Places
{
    public class AutoCompleteRequest
    {
        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("key")]
        public string ApiKey => GooglePlacesConfigs.ApiKey;

        [JsonProperty("offset", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Offset { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("radius", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Radius { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("components")]
        public string Components { get; set; }

        [JsonProperty("types")]
        public string Types { get; set; }
    }
}
