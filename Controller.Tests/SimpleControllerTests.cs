using Companies.Presemtation.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Controller.Tests;

public class SimpleControllerTests
{
    [Fact]
    public async void GetCompany_Should_Return400()
    {
        var sut = new SimpleController();

        var res = await sut.GetCompany();
        var resultType = res.Result as BadRequestObjectResult;

        Assert.IsType<BadRequestObjectResult>(resultType);
        Assert.Equal(StatusCodes.Status400BadRequest, resultType.StatusCode);

    }

    [Fact]
    public async Task GetCompany_IfNotAuth_ShouldReturd400BadRequest()
    {
        //var httpContextMock = new Mock<HttpContext>();
        //httpContextMock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(false);
        var httpContext = Mock.Of<HttpContext>(x => x.User.Identity.IsAuthenticated == false);

        var controllerContext = new ControllerContext
        {
            HttpContext = httpContext,
        };

        var sut = new SimpleController();
        sut.ControllerContext = controllerContext;

        var res = await sut.GetCompany();
        var resultType = res.Result as BadRequestObjectResult;

        Assert.IsType<BadRequestObjectResult>(resultType);
        Assert.Equal("is not auth", resultType.Value);
    }
}