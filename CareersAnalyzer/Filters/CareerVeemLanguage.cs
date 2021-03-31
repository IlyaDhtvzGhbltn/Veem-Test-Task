using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CareersAnalyzer.Filters
{
    class CareerVeemLanguage : IFilter
    {
        public void Invoke(IWebDriver driver, string[] criteria)
        {
            foreach (string lang in criteria) 
            {
                string langRegexTemplate = ConfigurationManager.AppSettings["VeemLangRegex"];
                string regexPattern = string.Format(langRegexTemplate, lang);
                Regex reg = new Regex(regexPattern);
                Match math = reg.Match(driver.PageSource);
                var elem = driver.FindElement(By.Id(math.Value));
                elem.Click();
            }
        }
    }
}
