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
        protected IWebDriver driver;

        public abstract void InitializeWebBrowser(string chromeExePath, string driverPath);
    }
}
