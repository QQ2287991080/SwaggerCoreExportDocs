using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerExtensions.Models
{

    public class ApiBuilder
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public RequestMethod RequestMethod { get; set; }
        public List<RequestParameter> RequestParameter { get; set; } = new List<RequestParameter>();
        public List<ResponeParmeter> ResponeParmeter { get; set; } = new List<ResponeParmeter>();
    }


    /// <summary>
    /// 请求参数信息
    /// </summary>
    public class RequestParameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string ParameterType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequried { get; set; }
    }
    /// <summary>
    /// 响应参数信息
    /// </summary>
    public class ResponeParmeter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string ParameterType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 请求方式
    /// </summary>
    public enum RequestMethod
    {
        Get,
        Put,
        Post,
        Delete,
        Options,
        Head,
        Patch,
        Trace
    }
}
