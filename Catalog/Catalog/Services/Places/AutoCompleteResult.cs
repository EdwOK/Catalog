using System.Collections.Generic;
using Newtonsoft.Json;

namespace Catalog.Services.Places
{
    public class AutoCompleteResult
    {
        [JsonProperty("predictions")]
        public List<Prediction> Predictions { get; set; }
    }
}
