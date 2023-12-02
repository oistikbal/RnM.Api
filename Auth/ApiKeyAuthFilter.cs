using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RnM.Api.Auth
{


    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        public const string ApiKeySectionName = "Authentication:ApiKey";
        public const string ApiKeyHeaderName = "ApiKey";

        private readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key Missing");
                return;
            }

            var apiKey = _configuration.GetValue<string>(ApiKeySectionName);

            if (!apiKey.Equals(extractedKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid Api Key");
                return;
            }
        }
    }
}
