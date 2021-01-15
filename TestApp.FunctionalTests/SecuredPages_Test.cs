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
using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace TestApp.FunctionalTests
{
    public class SecuredPages_Test : IClassFixture<CustomWebApplicationFactory>
    {
        HttpClient _client;
        public SecuredPages_Test(CustomWebApplicationFactory factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddTransient<ISaveImage, SaveImageMock>();
                });
            }).CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                HandleCookies = true
            });
        }

        private async Task LoginCookie()
        {
            var html = await _client.GetStringAsync("/account/login");
            var parser = new HtmlParser();
            var doc = await parser.ParseDocumentAsync(html);
            var requestVerficationToken = doc.GetElementsByName("__RequestVerificationToken").First() as IHtmlInputElement;

            var resultLogin = await _client.PostAsync(@"/account/login/", new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("Login", "admin@candyshop.tk"),
                new KeyValuePair<string, string>("Password", "123qwe123QWE@"),
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerficationToken.DefaultValue)
            }));

        }


        [Theory]
        [InlineData("/admin/additem")]
        [InlineData("/account/profile")]
        [InlineData("/basket/index")]
        public async Task GetSecuredPages(string url)
        {
            await LoginCookie();
            var result = await _client.GetAsync(url);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GetItemPage()
        {
            await LoginCookie();
            var result = await _client.GetStringAsync(@"/home/item/1");

            Assert.Contains("Add to cart", result);
        }
    }
}
