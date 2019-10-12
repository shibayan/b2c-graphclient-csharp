using Newtonsoft.Json;

namespace B2CGraphClient
{
    public class SignInName
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "value")]
        public string Value { get; set; }
    }
}