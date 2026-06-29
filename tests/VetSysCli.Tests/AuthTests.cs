using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace VetSysCli.Tests;

public class AuthTests
{
    [Fact]
    public async Task LoginEndpoint_ShouldAcceptDefaultAdminCredentials()
    {
        using var client = new HttpClient { BaseAddress = new System.Uri("http://localhost:5001") };
        var response = await client.PostAsJsonAsync("/api/auth/login", new { username = "admin", password = "123456@" });

        Assert.True(response.IsSuccessStatusCode);
    }
}
