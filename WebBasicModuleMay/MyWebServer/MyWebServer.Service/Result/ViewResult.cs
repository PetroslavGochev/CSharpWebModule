namespace MyWebServer.Service.Results
{
    using System.IO;
    using System.Linq;

    using MyWebServer.Service.Http;
    using MyWebServer.Service.Http.Enums;
    using MyWebServer.Service.Result;

    public class ViewResult : ActionResult
    {
        private const char PathSeparator = '/';

        public ViewResult(
            HttpResponse response,
            string viewName,
            string controllerName,
            object model) 
            : base(response)
        {
            this.GetHtml(viewName, controllerName, model);
        }

        private void GetHtml(string viewName, string controllerName, object model)
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var viewPath = Path.GetFullPath("./Views/" + viewName.TrimStart('/') + ".cshtml");

            if (!File.Exists(viewPath))
            {
                this.PrepareMissingViewError(viewPath);
                return;
            }

            var viewContent = File.ReadAllText(viewName);

            if( model != null)
            {
                viewContent = this.PopulateModel(viewContent, model);
            }

            this.PrepareContent(viewContent, HttpContentType.HtmlText);
        }

        private void PrepareMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;

            var errorMessage = $"View '{viewPath}' was not found.";

            this.PrepareContent(errorMessage, HttpContentType.PlainText);
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(pr => new
                {
                    Name = pr.Name,
                    Value = pr.GetValue(model),
                });

            foreach (var entry in data)
            {
                const string openingBrackets = "{{";
                const string closedBrackets = "}}";
                viewContent = viewContent.Replace($"{openingBrackets}{entry.Name}{closedBrackets}", entry.Value.ToString());
            }

            return viewContent;
        }
    }
}
