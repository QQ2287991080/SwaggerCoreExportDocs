using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using SwaggerCoreExportDocs.Abstracts;

namespace SwaggerCoreExportDocs
{
    public abstract class ExportDocs: IExportDocs
    {
        readonly OpenApiDocument _document;
        public ExportDocs(OpenApiDocument document)
        {

        }
    }
}
