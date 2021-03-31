using System;
using System.Configuration;
using CareersAnalyzer.WebBrowserTasks;
using McMaster.Extensions.CommandLineUtils;


namespace CareersAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = ConfigurationManager.AppSettings["Url"];
            string chrome = ConfigurationManager.AppSettings["ChromePath"];
            string driver = ConfigurationManager.AppSettings["DriverPath"];

            VacansionCounting countingTask = new VacansionCounting(url);
            countingTask.InitializeWebBrowser(chromePath: chrome, driverPath: driver);
            countingTask.TaskCompleted += compare;

            var application = new CommandLineApplication();
            application.Command("count", (a) => countingTask.Invoke(a));
            application.Execute(args);
        }

        static void compare(int await, int real) 
        {
            Console.WriteLine("Ожидалось:{0}, было выдано:{1}", await, real);
            if(await == real)
                Console.WriteLine(ConfigurationManager.AppSettings["Equal"]);
            if(await > real)
                Console.WriteLine(ConfigurationManager.AppSettings["AwaitedMore"]);
            if(await < real)
                Console.WriteLine(ConfigurationManager.AppSettings["AwaitedLess"]);
        }
    }
}
