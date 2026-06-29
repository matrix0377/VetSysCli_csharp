using Microsoft.AspNetCore.Mvc;
using VetSysCli.Api.Controllers;
using Xunit;

namespace VetSysCli.Tests;

public class AuthTests
{
    [Fact]
    public void LoginEndpoint_ShouldAcceptDefaultAdminCredentials()
    {
        var controller = new AuthController();

        var result = controller.Login(new LoginRequest { Username = "admin", Password = "123456@" });

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }
}
