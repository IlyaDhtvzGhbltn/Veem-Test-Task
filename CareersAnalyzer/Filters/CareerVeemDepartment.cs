using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareersAnalyzer.Filters
{
    class CareerVeemeDepartment : IFilter
    {
        public void Invoke(IWebDriver driver, params string[] criteria)
        {
            foreach (string dep in criteria) 
            {
                var elem = driver.FindElement(By.LinkText(dep));
                elem.Click();
            }
        }
    }
}
