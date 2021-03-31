using CareersAnalyzer.Filters;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareersAnalyzer.WebBrowserTasks
{
    abstract class SeleniunWebTask
    {
        protected Uri TargetUri;
        protected string ChromeBrowserPath;
        protected string WebDriverPath;
        protected IWebDriver driver;

        public SeleniunWebTask(string chromeBrowserPath, string webDriverPath)
        {
            ChromeBrowserPath = chromeBrowserPath;
            WebDriverPath = webDriverPath;
        }

        public abstract void InitializeWebBrowser();
    }
}
