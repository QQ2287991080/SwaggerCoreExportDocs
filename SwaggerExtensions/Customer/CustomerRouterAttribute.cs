
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerExtensions.Customer
{
    /// <summary>
    /// 自定义路由特性
    /// </summary>
    [AttributeUsage( AttributeTargets.Class,AllowMultiple =true,Inherited =true)]
    public class CustomerRouterAttribute :RouteAttribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 默认情况
        /// </summary>
        /// <param name="controller"></param>
        public CustomerRouterAttribute(string controller="[controller]"):base($"/api/{controller}")
        {

        }

        public CustomerRouterAttribute(SwaggerVersion version ,string controller = "[controller]") : base($"/api/{version}/{controller}")
        {
            GroupName = version.ToString();
        }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }


    }
    /// <summary>
    /// 用户自定义枚举版本
    /// </summary>
    public enum SwaggerVersion
    {
        /// <summary>
        /// v1版本
        /// </summary>
        v1 = 1,
        /// <summary>
        /// v2版本
        /// </summary>
        v2 = 2
    }
}
