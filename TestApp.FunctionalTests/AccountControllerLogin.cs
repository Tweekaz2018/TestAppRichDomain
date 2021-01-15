using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.FunctionalTests
{
    public class AccountControllerLogin : IClassFixture<CustomWebApplicationFactory>
    {
        HttpClient _client;
        public AccountControllerLogin(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task Login()
        {
            var html = await _client.GetStringAsync("/account/login");
            var parser = new HtmlParser();
            var doc = await parser.ParseDocumentAsync(html);
            var requestVerficationToken = doc.GetElementsByName("__RequestVerificationToken").First() as IHtmlInputElement;

            var result = await _client.PostAsync(@"/account/login/", new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("Login", "admin@candyshop.tk"),
                new KeyValuePair<string, string>("Password", "123qwe123QWE@"),                
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerficationToken.DefaultValue)
            }));

            Assert.Equal(HttpStatusCode.Redirect, result.StatusCode);
        }

        [Fact]
        public async Task Login_Fake_Data()
        {
            var html = await _client.GetStringAsync("/account/login");
            var parser = new HtmlParser();
            var doc = await parser.ParseDocumentAsync(html);
            var requestVerficationToken = doc.GetElementsByName("__RequestVerificationToken").First() as IHtmlInputElement;

            var result = await _client.PostAsync(@"/account/login/", new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("Login", "admin@candyshop.tk"),
                new KeyValuePair<string, string>("Password", "123qwe123qwe"),
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerficationToken.DefaultValue)
            }));

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
