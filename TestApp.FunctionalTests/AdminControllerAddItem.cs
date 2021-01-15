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
    public class AdminControllerAddItem : IClassFixture<CustomWebApplicationFactory>
    {
        HttpClient _client;
        public AdminControllerAddItem(CustomWebApplicationFactory factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandlerWithUserRole>(
                            "Test", options => { });
                    services.AddTransient<ISaveImage, SaveImageMock>();
                });
            }).CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task AddItem_Test()
        {
            var file = File.OpenRead(@"Helpers/test-image.jpg");
            var streamContent = new StreamContent(file);
            var form = new MultipartFormDataContent();
            form.Add(streamContent, "Image", "image.jpg");
            form.Add(new StringContent("Title"), "title");
            form.Add(new StringContent("descr"), "Description");
            form.Add(new StringContent("123"), "Price");

            var responce = await _client.PostAsync("/admin/AddItem", form);

            Assert.Equal(HttpStatusCode.Redirect, responce.StatusCode);
        }
    }
}
