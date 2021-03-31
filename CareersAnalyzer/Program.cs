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
            VacansionCounting countingTask = new VacansionCounting(uriStr: url, chromeBrowserPath: chrome, webDriverPath: driver);
            countingTask.TaskCompleted += compare;


            var application = new CommandLineApplication();
            application.Name = "Veem Careers Analyser";
            application.Command("count", (command) => 
            {   
                command.Description = "Command gets html from website and counts available positions under criteria.";
                countingTask.Invoke(command);
            });
            application.HelpOption("-?|-h|--help");
            application.Execute(args);

            Console.ReadKey();
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
