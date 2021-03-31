using CareersAnalyzer.Filters;
using McMaster.Extensions.CommandLineUtils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverExtensions;

namespace CareersAnalyzer.WebBrowserTasks
{
    class VacansionCounting : SeleniunWebTask
    {
        public int Count { get; private set; }
        public delegate void CountCompleted(int awaitedVacansionsCount, int realVacansionsCount);
        public event CountCompleted TaskCompleted;

        private CareerVeemLanguage LangFilter = new CareerVeemLanguage();
        private CareerVeemeDepartment DepartmentFilter = new CareerVeemeDepartment();

        private string langXpath;
        private string departsXpath;
        private string clearFiltersXpath;
        private string awaliableVacansions;

        #region fields
        private string LangXpath 
        { 
            get 
            { 
                return ConfigurationManager.AppSettings["VeemLangXpath"]; 
            }
            set 
            {
                langXpath = value;
            } 
        }

        private string DepartsXpath
        {
            get
            {
                return ConfigurationManager.AppSettings["VeemDepartmentXpath"];
            }
            set
            {
                departsXpath = value;
            }
        }

        private string ClearFiltersXpath
        {
            get
            {
                return ConfigurationManager.AppSettings["VeemClearFilteresXpath"];
            }
            set
            {
                clearFiltersXpath = value;
            }
        }
        
        private string AwaliableVacansions
        {
            get
            {
                return ConfigurationManager.AppSettings["VeemAwaliablePositionsRegex"];
            }
            set
            {
                awaliableVacansions = value;
            }
        }
        #endregion

        public VacansionCounting(string uriStr)
        {
            TargetUri = new Uri(uriStr);
        }
        public override void InitializeWebBrowser(string chromePath, string driverPath)
        {
            var options = new ChromeOptions();
            options.BinaryLocation = chromePath;
            this.driver = new ChromeDriver(driverPath, options);
        }


        public void Invoke(CommandLineApplication app) 
        {
            CommandOption fullScreenOpt = app.Option("-fullscreen", null, CommandOptionType.SingleValue);
            CommandOption languageOpt = app.Option("-language", null, CommandOptionType.SingleValue);
            CommandOption roleOpt = app.Option("-role", null, CommandOptionType.SingleValue);
            CommandOption awaitedOpt = app.Option("-awaited", null, CommandOptionType.SingleValue);
            CommandOption closeOpt = app.Option("-close", null, CommandOptionType.SingleOrNoValue);

            app.OnExecute(() =>
            {
                int awaited = 0;
                bool fullScreen = false;
                bool closeWhenDone = false;
                string role = roleOpt.Value();
                string language = languageOpt.Value();
                int.TryParse(awaitedOpt.Value(), out awaited);

                bool.TryParse(closeOpt.Value(), out closeWhenDone);
                bool.TryParse(fullScreenOpt.Value(), out fullScreen);
                if (fullScreen)
                    driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(TargetUri);

                driver.WaitElement(ClearFiltersXpath);
                driver.Scroll(ClearFiltersXpath);
                driver.ClickXp(LangXpath);

                LangFilter.Invoke(driver, new string[] { language });
                driver.ClickXp(LangXpath);
                driver.ClickXp(DepartsXpath);

                DepartmentFilter.Invoke(driver, new string[] { role });
                Count = driver.GetElementsCount(AwaliableVacansions);

                TaskCompleted.Invoke(awaitedVacansionsCount: awaited, realVacansionsCount: Count);
                if (closeWhenDone)
                    driver.Close();
            });
        }
    }
}
