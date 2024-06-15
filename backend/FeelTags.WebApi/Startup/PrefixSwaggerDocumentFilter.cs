using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FeelTags.WebApi.Startup
{
    public class PrefixSwaggerDocumentFilter(IHttpContextAccessor httpContextAccessor) : IDocumentFilter
    {
        private const string XForwardedPrefix = "X-Forwarded-Prefix";

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            IHeaderDictionary? headers = httpContextAccessor.HttpContext?.Request.Headers;
            string? prefix = (string?)headers?[XForwardedPrefix];
            if (prefix is null)
            {
                return;
            }
            swaggerDoc.Servers.Add(new OpenApiServer { Url = $"/{prefix}" });
        }
    }
}
