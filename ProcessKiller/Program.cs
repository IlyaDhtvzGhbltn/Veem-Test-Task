using McMaster.Extensions.CommandLineUtils;
using ProcessKiller.Operations;
using ProcessKiller.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessKiller
{
    class Program
    {
        static IOutput output = new ConsoleOut();

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionCatcher;
            var application = new CommandLineApplication();
            application.Command("kill", (a) => new KillByTimer(output).Invoke(a));
            application.Execute(args);

            Console.ReadKey();
        }

        static void UnhandledExceptionCatcher(object sender, UnhandledExceptionEventArgs e)
        {
            output.Error(e.ExceptionObject.ToString());
            var ex = e.ExceptionObject as Exception;
            output.Error(ex.StackTrace);
        }

    }
}