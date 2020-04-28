using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerExtensions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenXmlDocsController : ControllerBase
    {
        [HttpGet("Create")]
        public void Create()
        {
            // 创建流
            using (MemoryStream mem = new MemoryStream())
            {
                // 创建文件
                using (WordprocessingDocument wordDocument =
                    WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true))
                {
                    // 添加文档主要部分
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                    // Create the document structure and add some text.
                    mainPart.Document = new Document();
                    Body docBody = new Body();
                    //段落
                    {
                        Paragraph p = new Paragraph();
                        Run r = new Run();
                        Text t = new Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent quam augue, tempus id metus in, laoreet viverra quam. Sed vulputate risus lacus, et dapibus orci porttitor non.");
                        r.Append(t);
                        p.Append(r);
                        docBody.Append(p);
                    }
                    //段落
                    {
                        Paragraph p = new Paragraph();
                        // Run 1
                        Run r1 = new Run();
                        Text t1 = new Text("Pellentesque ") { Space = SpaceProcessingModeValues.Preserve };
                        // The Space attribute preserve white space before and after your text
                        r1.Append(t1);
                        p.Append(r1);

                        // Run 2 - Bold
                        Run r2 = new Run();
                        RunProperties rp2 = new RunProperties();
                        rp2.Bold = new Bold();
                        // Always add properties first
                        r2.Append(rp2);
                        Text t2 = new Text("commodo ") { Space = SpaceProcessingModeValues.Preserve };
                        r2.Append(t2);
                        p.Append(r2);

                        // Run 3
                        Run r3 = new Run();
                        Text t3 = new Text("rhoncus ") { Space = SpaceProcessingModeValues.Preserve };
                        r3.Append(t3);
                        p.Append(r3);

                        // Run 4 – Italic
                        Run r4 = new Run();
                        RunProperties rp4 = new RunProperties();
                        rp4.Italic = new Italic();
                        // Always add properties first
                        r4.Append(rp4);
                        Text t4 = new Text("mauris") { Space = SpaceProcessingModeValues.Preserve };
                        r4.Append(t4);
                        p.Append(r4);

                        // Run 5
                        Run r5 = new Run();
                        Text t5 = new Text(", sit ") { Space = SpaceProcessingModeValues.Preserve };
                        r5.Append(t5);
                        p.Append(r5);

                        // Run 6 – Italic , bold and underlined
                        Run r6 = new Run();
                        RunProperties rp6 = new RunProperties();
                        rp6.Italic = new Italic();
                        rp6.Bold = new Bold();
                        rp6.Underline = new Underline();
                        // Always add properties first
                        r6.Append(rp6);
                        Text t6 = new Text("amet ") { Space = SpaceProcessingModeValues.Preserve };
                        r6.Append(t6);
                        p.Append(r6);

                        // Run 7
                        Run r7 = new Run();
                        Text t7 = new Text("faucibus arcu ") { Space = SpaceProcessingModeValues.Preserve };
                        r7.Append(t7);
                        p.Append(r7);

                        // Run 8 – Red color
                        Run r8 = new Run();
                        RunProperties rp8 = new RunProperties();
                        rp8.Color = new Color() { Val = "FF0000" };
                        // Always add properties first
                        r8.Append(rp8);
                        Text t8 = new Text("porttitor ") { Space = SpaceProcessingModeValues.Preserve };
                        r8.Append(t8);
                        p.Append(r8);

                        // Run 9
                        Run r9 = new Run();
                        Text t9 = new Text("pharetra. Maecenas quis erat quis eros iaculis placerat ut at mauris.") { Space = SpaceProcessingModeValues.Preserve };
                        r9.Append(t9);
                        p.Append(r9);
                        // Add your paragraph to docx body
                        docBody.Append(p);
                    }
                    {
                        Paragraph p = new Paragraph();
                        ParagraphProperties pp = new ParagraphProperties();
                        pp.Justification = new Justification() { Val = JustificationValues.Center };
                        // Add paragraph properties to your paragraph
                        p.Append(pp);
                        // Run
                        Run r = new Run();
                        Text t = new Text("Nam eu tortor ut mi euismod eleifend in ut ante. Donec a ligula ante. Sed rutrum ex quam. Nunc id mi ultricies, vestibulum sapien vel, posuere dui.") { Space = SpaceProcessingModeValues.Preserve };
                        r.Append(t);
                        p.Append(r);
                        // Add your paragraph to docx body
                        docBody.Append(p);
                    }
                    //{
                    //    // Heading 1
                    //    StyleRunProperties styleRunPropertiesH1 = new StyleRunProperties();
                    //    Color color1 = new Color() { Val = "2F5496" };
                    //    // Specify a 16 point size. 16x2 because it’s half-point size
                    //    DocumentFormat.OpenXml.Wordprocessing.FontSize fontSize1 = new DocumentFormat.OpenXml.Wordprocessing.FontSize();
                    //    fontSize1.Val = new StringValue("32");

                    //    styleRunPropertiesH1.Append(color1);
                    //    styleRunPropertiesH1.Append(fontSize1);
                    //    // Check above at the begining of the word creation to check where mainPart come from
                    //    AddStyleToDoc(mainPart, "heading1", "heading 1", styleRunPropertiesH1);

                    //    // Heading 2
                    //    StyleRunProperties styleRunPropertiesH2 = new StyleRunProperties();
                    //    Color color2 = new Color() { Val = "2F5496" };
                    //    // Specify a 13 point size. 16x2 because it’s half-point size
                    //    DocumentFormat.OpenXml.Wordprocessing.FontSize fontSize1 = new DocumentFormat.OpenXml.Wordprocessing.FontSize();
                    //    fontSize1.Val = new StringValue("26");

                    //    styleRunPropertiesH1.Append(color1);
                    //    styleRunPropertiesH1.Append(fontSize1);
                    //    AddStyleToDoc(mainPart, "heading2", "heading 2", styleRunPropertiesH1);

                    //    // Create your heading1 into docx
                    //    Paragraph pH1 = new Paragraph();
                    //    ParagraphProperties ppH1 = new ParagraphProperties();
                    //    ppH1.ParagraphStyleId = new ParagraphStyleId() { Val = "heading1" };
                    //    ppH1.SpacingBetweenLines = new SpacingBetweenLines() { After = "0" };
                    //    pH1.Append(ppH1);
                    //    // Run Heading1
                    //    Run rH1 = new Run();
                    //    Text tH1 = new Text("First Heading") { Space = SpaceProcessingModeValues.Preserve };
                    //    rH1.Append(tH1);
                    //    pH1.Append(rH1);
                    //    // Add your heading to docx body
                    //    docBody.Append(pH1);

                    //    // Create your heading2 into docx
                    //    Paragraph pH2 = new Paragraph();
                    //    ParagraphProperties ppH2 = new ParagraphProperties();
                    //    ppH2.ParagraphStyleId = new ParagraphStyleId() { Val = "heading2" };
                    //    ppH2.SpacingBetweenLines = new SpacingBetweenLines() { After = "0" };
                    //    pH2.Append(ppH2);
                    //    // Run Heading2
                    //    Run rH2 = new Run();
                    //    Text tH2 = new Text("Second Heading") { Space = SpaceProcessingModeValues.Preserve };
                    //    rH2.Append(tH2);
                    //    pH2.Append(rH2);
                    //    // Add your heading2 to docx body
                    //    docBody.Append(pH2);
                    //}
                }

                // Download File
                //Context.AppendHeader("Content-Disposition", String.Format("attachment;filename=\"0}.docx\"", MyDocxTitle));
                //mem.Position = 0;
                //mem.CopyTo(Context.Response.OutputStream);
                //Context.Response.Flush();
                //Context.Response.End();
            }
        }

        // 将样式应用于段落。
        public static void AddStyleToDoc(WordprocessingDocument mainPart, string styleid, string stylename, StyleRunProperties styleRunProperties)
        {

            //获取此文档的样式部分。
            StyleDefinitionsPart part =
                mainPart.MainDocumentPart.StyleDefinitionsPart;

            // 如果“样式”部分不存在，请添加它，然后添加样式。
            if (part == null)
            {
                part = AddStylesPartToPackage(mainPart);
                AddNewStyle(part, styleid, stylename, styleRunProperties);
            }
            else
            {
                // 如果样式不在文档中，请添加它。
                if (IsStyleIdInDocument(mainPart, styleid) != true)
                {
                    //在styleid上没有匹配项，因此让我们尝试样式名称。
                    string styleidFromName = GetStyleIdFromStyleName(mainPart, stylename);
                    if (styleidFromName == null)
                    {
                        AddNewStyle(part, styleid, stylename, styleRunProperties);
                    }
                    else
                        styleid = styleidFromName;
                }
            }

        }

        // Return styleid that matches the styleName, or null when there's no match.
        public static string GetStyleIdFromStyleName(WordprocessingDocument doc, string styleName)
        {
            StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
            string styleId = stylePart.Styles.Descendants<StyleName>()
                .Where(s => s.Val.Value.Equals(styleName) &&
                    (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
            return styleId;
        }

        // 将StylesDefinitionsPart添加到文档。 返回对其的引用。
        public static StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument mainPart)
        {
            StyleDefinitionsPart part;
            part = mainPart.AddNewPart<StyleDefinitionsPart>();
            DocumentFormat.OpenXml.Wordprocessing.Styles root = new DocumentFormat.OpenXml.Wordprocessing.Styles();
            root.Save(part);
            return part;
        }

        public static bool IsStyleIdInDocument(WordprocessingDocument mainPart, string styleid)
        {
            // 获取对此文档的Styles元素的访问权限。
            DocumentFormat.OpenXml.Wordprocessing.Styles s = mainPart.MainDocumentPart.StyleDefinitionsPart.Styles;

            // 检查是否有样式以及有多少种。
            int n = s.Elements<DocumentFormat.OpenXml.Wordprocessing.Style>().Count();
            if (n == 0)
                return false;

            // 在styleid上查找匹配项。
            DocumentFormat.OpenXml.Wordprocessing.Style style = s.Elements<DocumentFormat.OpenXml.Wordprocessing.Style>()
                .Where(st => (st.StyleId == styleid) && (st.Type == StyleValues.Paragraph))
                .FirstOrDefault();
            if (style == null)
                return false;
            return true;
        }

        // 使用指定的styleid和stylename创建一个新样式，并将其添加到指定的样式定义部分。
        private static void AddNewStyle(StyleDefinitionsPart styleDefinitionsPart, string styleid, string stylename, StyleRunProperties styleRunProperties)
        {
            // 获得对样式部分的根元素的访问。
            DocumentFormat.OpenXml.Wordprocessing.Styles styles = styleDefinitionsPart.Styles;

            // 创建一个新的段落样式并指定一些属性。
            DocumentFormat.OpenXml.Wordprocessing.Style style = new DocumentFormat.OpenXml.Wordprocessing.Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            style.Append(new StyleName() { Val = stylename });
            style.Append(new BasedOn() { Val = "Normal" });
            style.Append(new NextParagraphStyle() { Val = "Normal" });
            style.Append(new UIPriority() { Val = 900 });

            // 创建StyleRunProperties对象并指定一些运行属性。


            // 将运行属性添加到样式
            // 这里我们使用OuterXml。 如果您两次使用相同的变量，则会出现错误。 因此，请确保只需插入xml即可解决该错误。
            //style.Append(styleRunProperties.OuterXml);

            //将样式添加到样式部分
            styles.Append(style);
        }
    }
}