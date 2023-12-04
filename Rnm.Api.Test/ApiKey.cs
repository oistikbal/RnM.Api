using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RnM.Api;
using RnM.Api.Auth;

namespace RnM.Api.Test
{
    [TestClass]
    public class ApiTest
    {

        //Tried to add test db for the tests but failed, so only apikey check is done.

        [TestMethod]
        public async Task TestApiKeyMissing()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync("");
            var stringResut = await response.Content.ReadAsStringAsync();

            Assert.AreNotEqual(ApiKeyAuthFilter.KeyMissing, stringResut);
        }

        [TestMethod]
        public async Task TestApiKeyInvalid()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var headers = httpClient.DefaultRequestHeaders;
            headers.Add(ApiKeyAuthFilter.ApiKeyHeaderName, "dasdasdasd");

            var response = await httpClient.GetAsync("");
            var stringResut = await response.Content.ReadAsStringAsync();

            Assert.AreNotEqual(ApiKeyAuthFilter.KeyInvalid, stringResut);
        }

        [TestMethod]
        public async Task TestApiKeyTrue()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var env = webAppFactory.Services.GetRequiredService<IConfiguration>();
            var headers = httpClient.DefaultRequestHeaders;
            headers.Add(ApiKeyAuthFilter.ApiKeyHeaderName, env.GetValue<string>(ApiKeyAuthFilter.ApiKeySectionName));

            var response = await httpClient.GetAsync("/Character");
            var stringResut = await response.Content.ReadAsStringAsync();

            Assert.AreNotEqual(ApiKeyAuthFilter.KeyInvalid, stringResut);
            Assert.AreNotEqual(ApiKeyAuthFilter.KeyMissing, stringResut);
        }
    }
}