using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareersAnalyzer.Filters
{
    interface IFilter
    {
        void Invoke(IWebDriver driver, string[] criteria);
    }
}
