using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework
{
    public partial class ViewEngine : IViewEngine
    {
        public string GetHtml(string templateHtml, object model)
        {
            var methodCode = PrepareCSharpCode(templateHtml);
            string code = $@"using System;
using System.Text;
using System.Linq;
using System.Collections.Generic
namespace AppViewNamespace
{{
    public class AppViewCode : IView
    {{
        public string GetHtml(object model)
        {{
            var html = new StringBuilder(); 
             
              {methodCode}            

           return html.ToString();
        }}
    }}
}}";

            IView view = GetInstanceFromCode(code, model);
            string html = view.GetHtml(model);
            return html;
        }

        private IView GetInstanceFromCode(string code, object model)
        {
            var compilation =
            CSharpCompilation.Create("AppViewAssembly")
                 .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                 .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location))
                 .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                 .AddReferences(MetadataReference.CreateFromFile(model.GetType().Assembly.Location));
            var libraries = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();
            foreach (var library in libraries)
            {
                compilation = compilation.AddReferences(
                    MetadataReference.CreateFromFile(Assembly.Load(library).Location));
            }
            compilation = compilation.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(code));

            var memoryStream = new MemoryStream();
            var compilationResult = compilation.Emit(memoryStream);
            if (!compilationResult.Success)
            {
                return new ErrorView(compilationResult.Diagnostics
                    .Where(x => x.Severity == DiagnosticSeverity.Error)
                    .Select(x => x.GetMessage()));
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            var assemblyByteArray = memoryStream.ToArray();
            var assembly = Assembly.Load(assemblyByteArray);
            var type = assembly.GetType("AppViewNamespace.AppViewCode");
            var instance = Activator.CreateInstance(type) as IView;
            return instance;
        }

        private string PrepareCSharpCode(string templateHtml)
        {
            var supportedOperators = new[] { "if", "for", "foreach", "else" };
            StringBuilder cSharpCode = new StringBuilder();
            StringReader reader = new StringReader(templateHtml);
            string line;
            while((line = reader.ReadLine()) != null)
            {
                if(line.TrimStart().StartsWith("{") ||
                    line.TrimStart().StartsWith("}"))
                {
                    cSharpCode.AppendLine(line);
                }
                else if(supportedOperators.Any(x=> line.TrimStart().StartsWith("@" + x)))
                {
                    var indexOfAt = line.IndexOf("@");
                    line = line.Remove(indexOfAt, 1);
                    cSharpCode.AppendLine(line);
                }
                else if(line.Contains("@"))
                {
                    while (line.Contains("@"))
                    {
                        var atSignLocation = line.IndexOf("@");
                    }
                }
                else
                {
                    cSharpCode.AppendLine($"html.AppendLine(@\"{ line.Replace("\"", "\"\"")}\");");
                }
            }

            return cSharpCode.ToString();
        }
    }
}
