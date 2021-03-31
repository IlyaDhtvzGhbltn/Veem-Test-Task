using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessKiller.Output
{
    interface IOutput
    {
        void Write(string message);
    }
}
