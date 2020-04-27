using SwaggerCoreExportDocs.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerCoreExportDocs.Core
{
    public class ApiBuilder : IApiBuilder
    {
        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public RequestMethod RequestMethod => throw new NotImplementedException();

        public DocsParameter RequestParameter => throw new NotImplementedException();

        public DocsParameter ReturnParameter => throw new NotImplementedException();
    }
}
