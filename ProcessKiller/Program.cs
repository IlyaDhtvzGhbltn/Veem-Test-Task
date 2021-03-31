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
        static void Main(string[] args)
        {
            var application = new CommandLineApplication();
            application.Command("kill", (a) => new KillByTimer(new ConsoleOut()).Invoke(a));
            application.Execute(args);
        }
    }
}
