using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebDriverExtensions
{
    public static class ChromeDriverExtensions
    {
        public static void WaitElement(this IWebDriver driver, string xPath, int timeoutSec = 30) 
        {
            if (string.IsNullOrWhiteSpace(xPath))
                throw new ArgumentNullException();
            if (timeoutSec <= 0)
                throw new ArgumentNullException();

            while (timeoutSec > 0)
            {
                try
                {
                    var element = driver.FindElement(By.XPath(xPath));
                    break;
                }
                catch (Exception) { }
                finally 
                {
                    Thread.Sleep(1000);
                    timeoutSec -= 1;
                }
            }
        }

        public static void Scroll(this IWebDriver driver, string xPath) 
        {
            if (string.IsNullOrWhiteSpace(xPath))
                throw new ArgumentNullException();

            var element = driver.FindElement(By.XPath(xPath));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public static void ClickXp(this IWebDriver driver, string xPath) 
        {
            if (string.IsNullOrWhiteSpace(xPath))
                throw new ArgumentNullException();

            var elem = driver.FindElement(By.XPath(xPath));
            elem.Click();
        }

        public static int GetElementsCount(this IWebDriver driver, string criteria)
        {
            if (string.IsNullOrWhiteSpace(criteria))
                throw new ArgumentNullException();

            string content = driver.PageSource;
            Regex reg = new Regex(criteria);
            var mathCollections = reg.Matches(content);
            return mathCollections.Count;
        }
    }
}
