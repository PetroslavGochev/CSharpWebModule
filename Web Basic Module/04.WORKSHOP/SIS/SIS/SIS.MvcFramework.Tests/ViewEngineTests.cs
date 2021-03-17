using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace SIS.MvcFramework.Tests
{
    public class ViewEngineTests
    {
        [Theory]
        [InlineData("OnlyHtmlView")]
        [InlineData("ForForeachIfView")]
        [InlineData("ViewModelView")]
        public void GetHtmlTest(string testName)
        {
            var viewModel = new TestViewModel()
            {
                Year = 2020,
                Name = "Petroslav",
                Numbers = new List<int>() { 1, 10, 100, 1000 }
            };
            var testContent = File.ReadAllText("");
            var expectedResultContent = File.ReadAllText($"{testName}.Expected.html");

            IViewEngine viewEngine = new ViewEngine();
            var actionResult = viewEngine.GetHtml(testContent, viewModel);

            Assert.Equal(actionResult, expectedResultContent);
        }
     
    }
}
