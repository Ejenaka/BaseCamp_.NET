using System.IO;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace AutoShop.API.Helpers
{
    public class SwaggerHelper
    {
        public static string XmlCommentsFilePath 
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }     
    }
}