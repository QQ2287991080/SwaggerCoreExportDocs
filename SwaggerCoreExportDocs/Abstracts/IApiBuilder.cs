using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerCoreExportDocs.Abstracts
{
    public interface IApiBuilder
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 接口描述
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 请求方式
        /// </summary>
        RequestMethod RequestMethod { get; }
        /// <summary>
        /// 请求参数(可以为空)
        /// </summary>
        DocsParameter RequestParameter { get; }
        /// <summary>
        /// 返回参数(可以为空)
        /// </summary>
        DocsParameter ReturnParameter { get; }
    }
    /// <summary>
    /// 参数信息
    /// </summary>
    public class DocsParameter
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
