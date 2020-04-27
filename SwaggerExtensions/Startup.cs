using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using SwaggerExtensions.Customer;
using SwaggerExtensions.Middleware;
using SwaggerExtensions.Swagger.Version;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace SwaggerExtensions
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //多版本
            //services.AddApiVersioning();
            //services.AddVersionedApiExplorer();
            /*单版本配置*/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Test API",
                        Version = "1",
                        TermsOfService = new Uri("http://www.baidu.com"),
                        Description = "这是一个用来测试的接口",
                        License = new OpenApiLicense()
                        {
                            Name = "Zero",
                            Url = new Uri("http://www.baidu.com")
                        },
                        Contact = new OpenApiContact()
                        {
                            Email = "2287991080@qq.com",
                            Name = "Zero",
                            Url = new Uri("http://www.baidu.com")
                        }
                    });
                c.EnableAnnotations();
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SwaggerExtensions.xml");
                c.IncludeXmlComments(path);
            });
            /*多版本配置*/
            //services.AddSwaggerGen();
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //, IApiVersionDescriptionProvider provider
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.Test();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });
         
            
            app.UseSwaggerUI(c =>
            {


               
                #region 单版本配置
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");//不管什么情况这句话是必须需要的
                #endregion
                #region 多版本配置
                //foreach (var description in provider.ApiVersionDescriptions)
                //{
                //    c.SwaggerEndpoint($"/swagger/{description.ApiVersion.ToString()}/swagger.json", description.GroupName.ToLowerInvariant());
                    
                //}
                //foreach (var version in typeof(SwaggerVersion).GetEnumNames())
                //{
                //    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", description.GroupName.ToLowerInvariant());
                //}
                #endregion
                #region 替换HTML
                //var stream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Swagger/index.html"), FileMode.Open);//使用该方法的时候必须把文件放到bin目录下面
                ////var stream= GetType().Assembly.GetManifestResourceStream("SwaggerExtensions.Swagger.index.html");//加载当前程序中的网页 官网提供的这个方法是能够获取到的，我自己调试是无法获取到的。
                //if (stream!=null)
                //{
                //    c.IndexStream = () => stream;
                //}
                #endregion
                #region 一些功能是否显示
                // Core
                // Display
                c.DefaultModelExpandDepth(0);
                //c.DefaultModelRendering(ModelRendering.Example);//返回类型，优先展示方式，默认展示Example
                c.DefaultModelsExpandDepth(-1);//用于控制最下面展示的模型，-1是全部隐藏
                //c.DisplayOperationId();//控制操作列表中操作ID的显示
                c.DisplayRequestDuration();//控制试用请求的请求持续时间（以毫秒为单位）的显示
                c.DocExpansion(DocExpansion.List);//list 展开控制器下的所有接口，Full打开是所有接口的调试界面，None全部收缩,不配置的情况下是list
                c.EnableDeepLinking();//为标记和操作启用深度链接
                c.EnableFilter();//expression参数，在输入输入框中默认显示
                c.ShowExtensions();
                // Network
                //c.EnableValidator();//您可以使用此参数启用swagger ui的内置验证器（badge）功能，将其设置为null将禁用验证
                //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post);//启用的请求方式,如果不启用那么将无法调试
                #endregion
                #region 自定义样式
                //// Other
                //c.DocumentTitle = "这是我的测试文档";
                //c.InjectStylesheet("/css/swagger-ui.css");
                //c.InjectJavascript("/ext/custom-javascript.js");
                #endregion
            });
        }
    }
}
