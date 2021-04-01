using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessKiller.Output
{
    interface IOutput
    {
        void Information(string message);
        void Error(string message);
    }
}
