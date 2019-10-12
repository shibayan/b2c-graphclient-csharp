using Newtonsoft.Json;

namespace B2CGraphClient
{
    public class PasswordProfile
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "forceChangePasswordNextLogin")]
        public bool ForceChangePasswordNextLogin { get; set; }
    }
}