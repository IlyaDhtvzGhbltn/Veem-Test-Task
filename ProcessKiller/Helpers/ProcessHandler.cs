using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessKiller.Helpers
{
    class ProcessHandler
    {
        readonly string processName;
        int targetProcessId = -1;

        public ProcessHandler(string processName)
        {
            this.processName = processName;
        }

        public bool Exist() 
        {
            Process[] processCollection = Process.GetProcesses();
            if (targetProcessId == -1)
            {
                foreach (Process pro in processCollection)
                {
                    if (pro.ProcessName == processName)
                    {
                        targetProcessId = pro.Id;
                        return true;
                    }
                }
            }
            else 
            {
                try
                {
                    Process proc = Process.GetProcessById(targetProcessId);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
        
        public void Kill() 
        {
            Process proc = Process.GetProcessById(targetProcessId);
            proc.Kill();
        }
    }
}
