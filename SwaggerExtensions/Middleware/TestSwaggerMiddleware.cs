using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerExtensions.Middleware
{
    public class TestSwaggerMiddleware
    {
        private RequestDelegate _request;

        public TestSwaggerMiddleware(RequestDelegate request)
        {
            _request = request;
        }
        public async Task Invoke(HttpContext context)
        {
            var xx = context.Request;
            await _request(context);
        }
    }


    public static class AppExtensions
    {
        public static IApplicationBuilder Test(this IApplicationBuilder app)
        {

            return app.UseMiddleware<TestSwaggerMiddleware>();
        }
    }
}
