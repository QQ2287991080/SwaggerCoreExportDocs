using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerExtensions.Swagger.Version
{
    /// <summary>
    /// 调用以配置的Options实例。
    /// </summary>
    public class ConfigureSwaggerOptions:IConfigureOptions<SwaggerGenOptions>
    {
        //api版本提供者
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
            this.provider = provider;

        /// <summary>
        /// 注册SwaggerOptions实例
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo()
                    {
                        Title = $"Zero APIVersion {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description=description.ApiVersion.GroupVersion.ToString(),
                    });
            }
            
            options.EnableAnnotations();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SwaggerExtensions.xml");
            options.IncludeXmlComments(path);
        }
    }
}
