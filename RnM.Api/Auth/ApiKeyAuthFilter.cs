using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RnM.Api.Auth
{


    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        public const string ApiKeySectionName = "Authentication:ApiKey";
        public const string ApiKeyHeaderName = "ApiKey";
        public const string KeyInvalid = "Invalid Api Key";
        public const string KeyMissing= "Api Key Missing";

        private readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedKey))
            {
                context.Result = new UnauthorizedObjectResult(KeyMissing);
                return;
            }

            var apiKey = _configuration.GetValue<string>(ApiKeySectionName);

            if (!apiKey.Equals(extractedKey))
            {
                context.Result = new UnauthorizedObjectResult(KeyInvalid);
                return;
            }
        }
    }
}
