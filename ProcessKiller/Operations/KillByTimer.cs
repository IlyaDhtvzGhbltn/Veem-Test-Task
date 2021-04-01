using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading;
using ParseExtension.String;
using ProcessKiller.Helpers;
using ProcessKiller.Output;

namespace ProcessKiller.Operations
{
    class KillByTimer
    {
        private readonly IOutput outHandler;
        private const int msecInMins = 60000;
        private const int msecInSec = 1000;

        public KillByTimer(IOutput outHandler)
        {
            this.outHandler = outHandler;
        }
        public virtual void Invoke(CommandLineApplication app)
        {
            CommandOption nameOpt = app.Option("-name", null, CommandOptionType.SingleValue);
            CommandOption timeoutOpt = app.Option("-timeout", null, CommandOptionType.SingleValue);
            CommandOption intervalOpt = app.Option("-interval", null, CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                string process = nameOpt.Value();
                string timeout = timeoutOpt.Value();
                string interval = intervalOpt.Value();

                decimal timeoutMins = -1;
                decimal intervalMins = -1;

                timeout.TryParseAny(out timeoutMins);
                interval.TryParseAny(out intervalMins);

                bool valid = Validate(process, timeoutMins, intervalMins);
                if (!valid)
                {
                    throw new ArgumentException("One of parameters was invalid.");
                }

                ProcessHandler handler = new ProcessHandler(process);
                bool processExist = handler.Exist();
                if (!processExist)
                {
                    outHandler.Error(string.Format("Process '{0}' was not found.", process));
                }
                else
                {
                    decimal msecTimer = timeoutMins * msecInMins;
                    int msecInterval = (int)(intervalMins * msecInMins);
                    if(msecInterval == 0)
                        msecInterval = msecInSec;

                    while (msecTimer > 0)
                    {
                        string statusMessage = string.Format("{0} is running. {1}sec left before killing.", 
                            process,
                            msecTimer / msecInSec);

                        outHandler.Information(statusMessage);
                        msecTimer -= msecInterval;

                        Thread.Sleep(msecInterval);
                        if (!handler.Exist())
                        {
                            processExist = false;
                            break;
                        }
                    }

                    if (processExist)
                    {
                        handler.Kill();
                        outHandler.Information("Killing...");
                    }
                    outHandler.Information(string.Format("{0} was killed.", process));
                }
            });
        }

        bool Validate(string process, decimal timeoutMins, decimal intervalMins)
        {
            if (string.IsNullOrWhiteSpace(process))
                return false;
            if (timeoutMins <= 0 || intervalMins <= 0)
                return false;
            return true;
        }
    }
}
