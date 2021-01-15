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
    public class AccountControllerRegistration : IClassFixture<CustomWebApplicationFactory>
    {
        HttpClient _client;
        public AccountControllerRegistration(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task Registration()
        {
            var html = await _client.GetStringAsync("/account/register");
            var parser = new HtmlParser();
            var doc = await parser.ParseDocumentAsync(html);
            var requestVerficationToken = doc.GetElementsByName("__RequestVerificationToken").First() as IHtmlInputElement;

            var result = await _client.PostAsync(@"/account/register/", new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("Email", "tweeeee@email.ua"),
                new KeyValuePair<string, string>("Password", "123QWE123qwe@"),
                new KeyValuePair<string, string>("PasswordConfirm", "123QWE123qwe@"),
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerficationToken.DefaultValue)
            }));

            Assert.Equal(HttpStatusCode.Redirect, result.StatusCode);
        }

        [Fact]
        public async Task Registration_Fake_Data()
        {
            var html = await _client.GetStringAsync("/account/register");
            var parser = new HtmlParser();
            var doc = await parser.ParseDocumentAsync(html);
            var requestVerficationToken = doc.GetElementsByName("__RequestVerificationToken").First() as IHtmlInputElement;

            var result = await _client.PostAsync(@"/account/register/", new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("Email", "Admin"),
                new KeyValuePair<string, string>("Password", "123qwe123qwe"),
                new KeyValuePair<string, string>("PasswordConfirm", "123qwe123qwe"),
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerficationToken.DefaultValue)
            })); 

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Registration_Bad_Password_Data()
        {
            var html = await _client.GetStringAsync("/account/register");
            var parser = new HtmlParser();
            var doc = await parser.ParseDocumentAsync(html);
            var requestVerficationToken = doc.GetElementsByName("__RequestVerificationToken").First() as IHtmlInputElement;

            var result = await _client.PostAsync(@"/account/register/", new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("Email", "wer@i.ua"),
                new KeyValuePair<string, string>("Password", "123qwe123qwe"),
                new KeyValuePair<string, string>("PasswordConfirm", "123qwe123qwe"),
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerficationToken.DefaultValue)
            }));
            var resultHtml = await result.Content.ReadAsStringAsync();

            Assert.Contains("Passwords must have ", resultHtml);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
