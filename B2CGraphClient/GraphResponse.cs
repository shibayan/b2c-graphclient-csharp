using System.Collections.Generic;

using Newtonsoft.Json;

namespace B2CGraphClient
{
    public class GraphResponse<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "value")]
        public IList<T> Value { get; set; }
    }
}
