using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace B2CGraphClient
{
    public class User
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "objectId")]
        public string ObjectId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "accountEnabled")]
        public bool AccountEnabled { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mailNickname")]
        public string MailNickname { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "signInNames")]
        public IList<SignInName> SignInNames { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "creationType")]
        public string CreationType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "givenName")]
        public string GivenName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "passwordProfile")]
        public PasswordProfile PasswordProfile { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "passwordPolicies")]
        public string PasswordPolicies { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userPrincipalName")]
        public string UserPrincipalName { get; set; }

        public static User Create(string signInName, string password, string displayName, string givenName = null, string surname = null)
        {
            return new User
            {
                AccountEnabled = true,
                CreationType = "LocalAccount",

                SignInNames = new[]
                {
                    new SignInName { Type = "emailAddress", Value = signInName }
                },

                MailNickname = Guid.NewGuid().ToString(),

                PasswordProfile = new PasswordProfile { Password = password },
                PasswordPolicies = "DisablePasswordExpiration,DisableStrongPassword",

                DisplayName = displayName,
                GivenName = givenName,
                Surname = surname
            };
        }
    }
}