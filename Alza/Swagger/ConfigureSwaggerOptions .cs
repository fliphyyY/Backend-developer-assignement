using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Alza.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var desVersionText = description.ApiVersion.ToString().Equals("1.0")
                ? $"This is v{description.ApiVersion.ToString()} of WebAPI Application. This version contains three endpoints."
                : $"This is v{description.ApiVersion.ToString()} of WebAPI Application. This version contains one endpoint.";
            var info = new OpenApiInfo()
            {
                Title = $"WebApi Application v{description.ApiVersion.ToString()}",
                Version = description.ApiVersion.ToString(),
                Description = desVersionText,
                Contact = new OpenApiContact { Name = "MSc. Filip Ondrúšek", Email = "filip.ondrusek@gmail.com" },
                License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
