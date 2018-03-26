using System.Collections.Generic;
using Newtonsoft.Json;

namespace Catalog.Services.Places
{
    public class Prediction
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
    }
}
