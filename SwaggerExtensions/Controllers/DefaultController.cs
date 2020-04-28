using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.OpenApi.Models;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.POIFS.FileSystem;
using NPOI.XWPF.UserModel;
using SwaggerExtensions.Customer;
using SwaggerExtensions.Models;
using SwaggerExtensions.Swagger.Version;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace SwaggerExtensions.Controllers
{
    //[ApiVersion("1.0")]
    [SwaggerTag("默认控制器")]
    [Route("api/Default")]
    //[ApiController]
    //[Produces("application/json")]
    public class DefaultController : ControllerBase
    {

        private readonly ISwaggerProvider _provider;
        private readonly IWebHostEnvironment _env;
        public DefaultController(ISwaggerProvider provider, IWebHostEnvironment env)
        {
            _provider = provider;
            _env = env;
        }

        /// <summary>
        /// 获取id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            return new JsonResult("");
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        [SwaggerResponse(200, "操作成功", typeof(Person))]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public void Post([FromBody]Person person)
        {
            var xx = Request.PathBase;
            //var er = SwaggerVersion.v1.ToString();
            //var swagger = _provider.GetSwagger("v1");
            //var compoents = swagger.Components;
            //var tags = swagger.Tags;
            //var servers = swagger.Servers;
            //var security = swagger.SecurityRequirements;
            //var paths = swagger.Paths;
            //var xx2 = paths["/api/Default/Get"].Operations.Values;
            //var info = swagger.Info;
            //var ms = Word();
            //ms.Seek(0, SeekOrigin.Begin);
            //return File(ms, "application/msword", "测试");
        }
        [HttpPost]
        [Route("Add2")]
        [SwaggerResponse(200, "操作成功", typeof(Person))]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public void Post2([FromBody]List<Person> person)
        {
            Console.WriteLine("");
        }
            private MemoryStream Word()
        {
            //保存地址
            string savePath = _env.WebRootPath + "/doc/" + $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.doc";
            string fileName = string.Format("{0}", savePath, Encoding.UTF8);


            var xx = Request.PathBase;
            var er = SwaggerVersion.v1.ToString();
            var swagger = _provider.GetSwagger("v1");
            var compoents = swagger.Components;
            var tags = swagger.Tags;//所有的标题，用于创建导航属性
            var servers = swagger.Servers;
            var security = swagger.SecurityRequirements;
            var paths = swagger.Paths;
            var info = swagger.Info;


            Dictionary<string, string> tagDic = new Dictionary<string, string>();
            foreach (var item in tags)
            {
                tagDic.Add(item.Name, item.Description);
            }
            tagDic.Add("其它", "其它");

            using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                //创建一个文档对象
                XWPFDocument document = new XWPFDocument();
                //创建段落
                document.SetParagraph(Docxtor.GetDocxtor.ParagraphInstanceSetting(document, "测试", true, 19, "宋体", ParagraphAlignment.CENTER), 0);
                //创建表格
                XWPFTable firstXwpfTable = document.CreateTable(paths.Count * 2, 5);
                firstXwpfTable.Width = 5200;//总宽度
                firstXwpfTable.SetColumnWidth(0, 1300); /* 设置列宽 */
                firstXwpfTable.SetColumnWidth(1, 1100); /* 设置列宽 */
                firstXwpfTable.SetColumnWidth(2, 500); /* 设置列宽 */
                firstXwpfTable.SetColumnWidth(3, 1400); /* 设置列宽 */
                firstXwpfTable.SetColumnWidth(4, 900); /* 设置列宽 */
                firstXwpfTable.GetRow(0).GetCell(0).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, "接口名称", ParagraphAlignment.CENTER, 24, true));
                firstXwpfTable.GetRow(0).GetCell(1).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, "接口参数", ParagraphAlignment.CENTER, 24, true));
                firstXwpfTable.GetRow(0).GetCell(2).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, "请求方式", ParagraphAlignment.CENTER, 24, true));
                firstXwpfTable.GetRow(0).GetCell(3).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, "返回参数", ParagraphAlignment.CENTER, 24, true));
                firstXwpfTable.GetRow(0).GetCell(4).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, "备注", ParagraphAlignment.CENTER, 24, true));
                int index = 1;
                foreach (var item in paths)
                {
                    var value = item.Value.Operations;

                    var xx2 = paths[item.Key].Operations;


                    //设置值
                    firstXwpfTable.GetRow(index).GetCell(0).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, item.Key, ParagraphAlignment.CENTER, 24, false));


                    foreach (var operationType in xx2.Keys)
                    {
                        var crete = xx2[operationType];
                        var menthod = operationType.ToString();
                        var parmeters = crete.Parameters;
                        string requestParameter = "";

                        var para = new CT_P();
                        //设置单元格文本对齐
                        para.AddNewPPr().AddNewTextAlignment();

                        XWPFParagraph paragraph = new XWPFParagraph(para, firstXwpfTable.Body);//创建表格中的段落对象
                        paragraph.Alignment = ParagraphAlignment.LEFT;//文字显示位置,段落排列（左对齐，居中，右对齐）
                        foreach (var parameter in parmeters)
                        {
                            XWPFRun xwpfRun = paragraph.CreateRun();//创建段落文本对象
                            xwpfRun.SetText(parameter.Name + "-" + parameter.Schema.Type + "-" + parameter.Description ?? "");
                            xwpfRun.FontSize = 10;//字体大小
                            xwpfRun.SetColor("000000");//设置字体颜色--十六进制
                            xwpfRun.IsItalic = false;//是否设置斜体（字体倾斜）
                            xwpfRun.IsBold = false;//是否加粗
                            xwpfRun.AddBreak(BreakType.TEXTWRAPPING);
                            xwpfRun.SetFontFamily("宋体", FontCharRange.None);//设置字体（如：微软雅黑,华文楷体,宋体）
                            xwpfRun.SetTextPosition(24);//设置文本位置（设置两行之间的行间），从而实现table的高度设置效果 
                            ////参数名称+参数类型+参数描述
                            requestParameter += parameter.Name + "-" + parameter.Schema.Type + "-" + parameter.Description ?? "" + "</br>";
                        }
                        //请求参数
                        //firstXwpfTable.GetRow(index).GetCell(1).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, requestParameter, ParagraphAlignment.LEFT, 24, false));
                        firstXwpfTable.GetRow(index).GetCell(1).SetParagraph(paragraph);
                        //请求方式
                        firstXwpfTable.GetRow(index).GetCell(2).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, menthod, ParagraphAlignment.CENTER, 24, false));

                        var rep = crete.Responses;
                        string fanhuicanshu = "";
                        foreach (var key in rep.Keys)
                        {
                            if (key.Equals("application/json"))
                            {

                                var repValues = rep[key];
                                var repValues2 = rep["application/json"];

                                var cs = repValues.Content.Keys;

                                var scheam = rep[key].Content["application/json"].Schema.Items;
                                var scheam2 = scheam == null ? "" : scheam.Reference.ReferenceV3;
                                fanhuicanshu = scheam == null ? "" : scheam2.Substring(scheam2.LastIndexOf("/") + 1);
                                //foreach (var content in cs)
                                //{
                                //    var scheam = repValues.Content[content].Schema.Items;
                                //    var scheam2 = scheam==null ? "" : scheam.Reference.ReferenceV3;
                                //    fanhuicanshu = scheam==null ? "" : scheam2.Substring(scheam2.LastIndexOf("/") + 1);
                                //}
                            }
                            break;
                        }
                        //返回参数
                        firstXwpfTable.GetRow(index).GetCell(3).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, "返回参数", ParagraphAlignment.CENTER, 24, false));
                        //备注
                        firstXwpfTable.GetRow(index).GetCell(4).SetParagraph(Docxtor.GetDocxtor.SetTableParagraphInstanceSetting(document, firstXwpfTable, crete.Summary ?? "", ParagraphAlignment.CENTER, 24, false));
                    }
                    index++;
                }

                document.Write(fs);

                //byte[] bytes = new byte[fs.Length];
                //fs.Write(bytes, 0, (int)fs.Length);
                MemoryStream ms = new MemoryStream();
                return ms;
            }
        }

        
        [SwaggerOperation(Summary ="测试")]
        [HttpPost]
        //[Obsolete("不能使用")]
        [Route("Word2")]
        public MemoryStream Word2()
        {
            //保存地址
            string savePath = _env.WebRootPath + "/doc/" + $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.doc";
            string fileName = string.Format("{0}", savePath, Encoding.UTF8);


            var xx = Request.PathBase;
            var er = SwaggerVersion.v1.ToString();
            var swagger = _provider.GetSwagger("v1");
            var compoents = swagger.Components;
            var tags = swagger.Tags;//所有的标题，用于创建导航属性
            var servers = swagger.Servers;
            var security = swagger.SecurityRequirements;
            var paths = swagger.Paths;
            var info = swagger.Info;


            //遍历所有的组件取出对应返回模型数据
            Dictionary<string, List<ResponeParmeter>> compoentParmeter = new Dictionary<string, List<ResponeParmeter>>();
            foreach (var item in compoents.Schemas)
            {
                //分解参数
               var propertyDetails = GetProperty(item.Value.Properties);
                compoentParmeter.Add(item.Key, propertyDetails);
            }
            //遍历所有的标签，存储在字典集合中
            Dictionary<string, OpenApiTag> tagDic = new Dictionary<string, OpenApiTag>();
            //存放接口信息
            Dictionary<OpenApiTag, List<ApiBuilder>> builderApi = new Dictionary<OpenApiTag, List<ApiBuilder>>();
            OpenApiTag tag = new OpenApiTag()
            {
                Description = "其它",
                Name = "其它",
            };
            builderApi.Add(tag, new List<ApiBuilder>());
            tagDic.Add("其它", tag);
            foreach (var item in tags)
            {
                List<ApiBuilder> apis = new List<ApiBuilder>();
                builderApi.Add(item, apis);

                tagDic.Add(item.Name, item);
            }
            
            foreach (var item in paths)
            {
                //实例化一个接口
                ApiBuilder apiBuilder = new ApiBuilder();
                //接口名称 
                var apiName = item.Key;
                apiBuilder.Name = apiName;
               
                //接口的操作(请求方式，请求参数，响应参数，Summary)
                var apiOperations = paths[item.Key].Operations;

                
                //通过接口操作的所有key值去拿到对应操作的值
                foreach (var operationType in apiOperations.Keys)
                {
                    //某个操作
                    var operation = apiOperations[operationType];
                    //接口的描述
                    apiBuilder.Description = operation.Description;
                    //获取操作的请求方式
                    var menthod = operationType.ToString();
                    apiBuilder.RequestMethod = (RequestMethod)Enum.Parse(typeof(RequestMethod), menthod);
                    //获取接口的请求参数
                    var parmeters = operation.Parameters;
                    foreach (var parameter in parmeters)
                    {
                        RequestParameter reqParmeter = new RequestParameter();
                        reqParmeter.Description = parameter.Description ?? "";
                        reqParmeter.Name = parameter.Name;
                        reqParmeter.ParameterType = parameter.Schema.Type;
                        reqParmeter.IsRequried = parameter.Required;
                        apiBuilder.RequestParameter.Add(reqParmeter);
                    }
                    //获取接口的所有的响应方式
                    var rep = operation.Responses;
                    foreach (var key in rep.Keys)
                    {
                        var repValues = rep[key];
                        var cs = repValues.Content.Keys;
                        foreach (var c in cs)
                        {
                            var scheam = repValues.Content[c].Schema;
                            var scheamKey = scheam == null ? "" : (scheam.Reference == null ? "" : scheam.Reference.ReferenceV3);
                            string subScheamKey = scheam == null ? "" : scheamKey.Substring(scheamKey.LastIndexOf("/") + 1);
                            if (compoentParmeter.ContainsKey(subScheamKey))
                            {
                                apiBuilder.ResponeParmeter = compoentParmeter[subScheamKey];
                            }
                        }
                    }
                    //判断属于哪个模块下的
                    var tagName = operation.Tags[0].Name;
                    if (tagDic.ContainsKey(tagName))
                    {
                        var builderKey = tagDic[tagName];
                        builderApi[builderKey].Add(apiBuilder);
                    }
                    else
                    {
                        //如果没有tag属性那么放到其它里面
                        var builderKey = tagDic["其它"];
                        builderApi[builderKey].Add(apiBuilder);
                    }
                }
            }
            MemoryStream ms = new MemoryStream();
            return ms;
        }

        /// <summary>
        /// 获取属性字符串
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private List<ResponeParmeter> GetProperty(IDictionary<string, OpenApiSchema> properties)
        {
            List<ResponeParmeter> parmeters = new List<ResponeParmeter>();
            foreach (var item in properties)
            {
                ResponeParmeter respone = new ResponeParmeter();
                //拿到对应的schema
                var propertyValue = properties[item.Key];
                //如果是model类型的属性
                if (propertyValue.Items==null&&propertyValue.Reference!=null)//没有子集，用类型参考
                {
                    respone.IsReference = true;
                    respone.SchemaId = propertyValue.Reference.Id;
                }
                if (propertyValue.Items != null&&propertyValue.Reference==null)
                {
                    if (propertyValue.Items.Reference!=null)
                    {
                        respone.IsReference = true;
                        respone.SchemaId = propertyValue.Reference.Id;
                    }
                    else
                    {
                        respone.ParameterType = propertyValue.Items.Type;
                    }
                }
                
                {
                    respone.Description = propertyValue.Description;//备注
                    respone.Name = item.Key;//参数名
                    
                };
                parmeters.Add(respone);
            }
            return parmeters;
        }
    }


    public class Person
    {
        /// <summary>
        /// 年龄
        /// </summary>
       [Required]
        public int Age { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 多个姓名
        /// </summary>
        public List<string> Names { get; set; }
        public Children  Children { get; set; }
        public List<Children>  Childrens { get; set; }
    }
    public class Children
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public Children Children2 { get; set; }
    }
}