using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Supernova.Application.Abstraction.Contracts;
using Supernova.Test.Infrastructure;
using System.Security.Claims;

namespace Supernova.Test.WebApiTests;

public class WebApiTestBase : TestBase
{
    protected Mock<IHttpContextAccessor> _context;
    protected IMapper _mapper;
    protected Mock<OkObjectResult> _serviceResponseHelper;
    protected HttpContext _httpContext;
    public WebApiTestBase()
    {
        _context = new Mock<IHttpContextAccessor>();
        _mapper = SetMapper();
        _httpContext = SetHttpContext();
        _serviceResponseHelper = new Mock<OkObjectResult>();
    }
    private IMapper SetMapper()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetService<IMapper>();
    }
    #region Cache

    protected AppUserDto CurrentUser =>
        new()
        {
            Id = ANumber(),
            Name = AString(),
            Surname = AString()
        };

    #endregion

    #region HttpContext

    protected HttpContext SetHttpContext()
    {
        var request = SetUpHttpRequest();
        var httpContext = SetUpHttpContext(request);
        var identity = new ClaimsIdentity(Claims(), "mock");
        var principal = new ClaimsPrincipal(identity);
        principal.AddIdentity(identity);
        httpContext.User = principal;
        _context.Setup(x => x.HttpContext).Returns(httpContext);

        return httpContext;
    }

    protected List<Claim> Claims() => new List<Claim>() {
            new Claim("UserName", AString()),
            new Claim("FirstName", AString()),
            new Claim("LastName", AString()),
            new Claim("Title", AString()),
            new Claim("Mail", AString()),
            new Claim("Language", AString()),
            new Claim("IsAuthenticated", "true"),
            new Claim("jti", AGuid().ToString())
        };

    protected HttpContext SetUpHttpContext(HttpRequest httpRequest)
       => Mock.Of<HttpContext>(x => x.Request == httpRequest);

    protected HttpRequest SetUpHttpRequest()
    {
        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Scheme).Returns("http");
        request.Setup(x => x.HttpContext.Request.Headers).Returns(new HeaderDictionary());
        return request.Object;
    }
    #endregion

    #region Service Providers
    protected IServiceProvider SetUpServiceProvider()
    {
        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(x => x.GetService(typeof(OkObjectResult))).Returns(_serviceResponseHelper.Object);
        return serviceProviderMock.Object;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(WebApiTestBase).Assembly);
    }
    #endregion

    #region Random

    public string RandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    #endregion
}
