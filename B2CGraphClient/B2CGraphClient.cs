using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Newtonsoft.Json;

namespace B2CGraphClient
{
    public class B2CGraphClient
    {
        private readonly AuthenticationContext _authContext;
        private readonly ClientCredential _credential;

        public const string GraphEndpoint = "https://graph.windows.net/";
        public const string GraphVersion = "api-version=1.6";

        public string Tenant { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }

        private static AuthenticationResult _accessToken;
        private static readonly HttpClient _httpClient = new HttpClient();

        public B2CGraphClient(string tenant, string clientId, string clientSecret)
        {
            Tenant = tenant;
            ClientId = clientId;
            ClientSecret = clientSecret;

            _authContext = new AuthenticationContext("https://login.microsoftonline.com/" + Tenant);

            _credential = new ClientCredential(ClientId, ClientSecret);
        }

        public async Task<IList<User>> FindByNameAsync(string signInName)
        {
            if (signInName == null)
            {
                throw new ArgumentNullException(nameof(signInName));
            }

            var response = await GetAsync<GraphResponse<User>>("/users", $"$filter=signInNames/any(x:x/value eq '{WebUtility.UrlEncode(signInName)}')");

            return response.Value;
        }

        public Task<User> CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return PostAsync<User>("/users", user);
        }

        public Task DeleteUserAsync(string objectId)
        {
            if (objectId == null)
            {
                throw new ArgumentNullException(nameof(objectId));
            }

            return DeleteAsync<object>($"/users/{objectId}");
        }

        private Task<T> GetAsync<T>(string api, string query)
        {
            return SendRequestAsync<T>(HttpMethod.Get, api, query);
        }

        private Task<T> PostAsync<T>(string api, object payload)
        {
            return SendRequestAsync<T>(HttpMethod.Post, api, payload: payload);
        }

        private Task<T> PatchAsync<T>(string api, object payload)
        {
            return SendRequestAsync<T>(HttpMethod.Patch, api, payload: payload);
        }

        private Task<T> DeleteAsync<T>(string api)
        {
            return SendRequestAsync<T>(HttpMethod.Delete, api);
        }

        private async Task<T> SendRequestAsync<T>(HttpMethod method, string api, string query = null, object payload = null)
        {
            var accessToken = await AcquireAccessToken();

            var url = $"{GraphEndpoint}{Tenant}{api}?{GraphVersion}";

            if (!string.IsNullOrEmpty(query))
            {
                url += "&" + query;
            }

            var request = new HttpRequestMessage(method, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            if (payload != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                throw new B2CGraphApiException(content)
                {
                    StatusCode = response.StatusCode
                };
            }

            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(body);
        }

        private async Task<string> AcquireAccessToken()
        {
            if (_accessToken == null || _accessToken.ExpiresOn.UtcDateTime > DateTime.UtcNow.AddMinutes(-10))
            {
                _accessToken = await _authContext.AcquireTokenAsync(GraphEndpoint, _credential);
            }

            return _accessToken.AccessToken;
        }
    }
}