using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TestApp.FunctionalTests.Helper;
using TestAppRichDomain.Core.Interfaces;

namespace TestApp.FunctionalTests
{
    public class NotSecuredPages_Tests : IClassFixture<CustomWebApplicationFactory>
    {
        HttpClient _client;
        public NotSecuredPages_Tests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Theory]
        [InlineData("/account/Login")]
        [InlineData("/account/register")]
        [InlineData("/home/Index")]
        [InlineData("/home/item/1")]
        public async Task Get_NonSecured_Pages(string url)
        {
            var result = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [InlineData("/admin/additem")]
        [InlineData("/account/profile")]
        [InlineData("/basket/index")]
        public async Task GetSecuredPages(string url)
        {
            var result = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.Redirect, result.StatusCode);
        }
    }
}
