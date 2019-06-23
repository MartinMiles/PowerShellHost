using System;
using System.Management.Automation;

namespace PowerShellHost.Runner
{
    public class RunnerException : Exception
    {
        public PSDataCollection<ErrorRecord> Errors { get; private set; }

        public RunnerException(PSDataCollection<ErrorRecord> errors)
        {
            Errors = errors;
        }
    }
}
