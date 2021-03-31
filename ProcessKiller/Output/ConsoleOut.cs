using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessKiller.Output
{
    class ConsoleOut : IOutput
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
