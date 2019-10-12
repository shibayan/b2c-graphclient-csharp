using System.Threading.Tasks;

namespace B2CGraphClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var clientId = "";
            var clientSecret = "";
            var tenantId = "";

            var graphClient = new B2CGraphClient(tenantId, clientId, clientSecret);

            var user = User.Create("demo@example.com", "P@ssw0rd!", "demo user");

            await graphClient.CreateUserAsync(user);

            var result = await graphClient.FindByNameAsync("demo@example.com");
        }
    }
}
