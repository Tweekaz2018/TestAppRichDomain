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
    public class BasketControllerMakeOrder : IClassFixture<CustomWebApplicationFactory>
    {
        HttpClient _client;
        public BasketControllerMakeOrder(CustomWebApplicationFactory factory)
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

        [Fact]
        public async Task MakeOrder_From_Basket_Test()
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

            var result = await _client.PostAsync(@"/basket/makeorder/", new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("basketId", "1"),
                new KeyValuePair<string, string>("Address", "Address"),
                new KeyValuePair<string, string>("Comment", "Comment"),
            }));

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("We will contact u as soon as posible", await result.Content.ReadAsStringAsync());
        }
    }
}
