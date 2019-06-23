using System;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.PowerShell;
using PowerShellHost.Formatters;

namespace PowerShellHost.Runner
{
    public class PowerShellRunner : IPowerShellRunner
    {
        private readonly RunnerConfiguration _runnerConfiguration;

        private int _errorCount = 0;

        public PowerShellRunner(RunnerConfiguration runnerConfiguration)
        {
            _runnerConfiguration = runnerConfiguration 
                ?? throw new NullReferenceException("RunnerConfiguration is null. Please ensure you provide valid configuration into PowerShellRunner constructor");
        }

        public dynamic Run(string script)
        {
            using (var runspace = CreateRunspace())
            {
                runspace.Open();

                using (var powerShell = PowerShell.Create())
                {
                    powerShell.Runspace = runspace;

                    var command = new Command(script);
                    if (_runnerConfiguration.Parameters != null)
                    {
                        foreach (var parameter in _runnerConfiguration.Parameters)
                        {
                            command.Parameters.Add(new CommandParameter(parameter.Key, parameter.Value));
                        }
                    }

                    powerShell.Commands.AddCommand(command);

                    powerShell.Streams.Error.DataAdded += OnError;
                    powerShell.Streams.Debug.DataAdded += OnDebug;
                    powerShell.Streams.Warning.DataAdded += OnWarning;
                    powerShell.Streams.Progress.DataAdded += OnProgress;
                    powerShell.Streams.Verbose.DataAdded += OnVerbose;

                    var outputCollection = new PSDataCollection<PSObject>();

                    outputCollection.DataAdded += OnObjectOutput;

                    var invokeResult = powerShell.BeginInvoke<PSObject, PSObject>(null, outputCollection);

                    powerShell.EndInvoke(invokeResult);

                    if (_errorCount != 0)
                    {
                        throw new RunnerException(powerShell.Streams.Error);
                    }

                    return outputCollection.LastOrDefault();
                }
            }
        }

        private Runspace CreateRunspace()
        {
            var initial = InitialSessionState.CreateDefault();
            initial.ExecutionPolicy = ExecutionPolicy.Unrestricted;

            if (_runnerConfiguration.IsRemote)
            {
                var connectionInfo = new WSManConnectionInfo
                {
                    ComputerName = _runnerConfiguration.RemoteHostName,
                    AuthenticationMechanism = AuthenticationMechanism.Negotiate
                };

                return RunspaceFactory.CreateRunspace(connectionInfo);
            }

            return RunspaceFactory.CreateRunspace(initial);
        }

        private void OnObjectOutput(object sender, DataAddedEventArgs e)
        {
            if (_runnerConfiguration.OnObjectOutput == null)
            {
                return;
            }

            var psDataCollection = sender as PSDataCollection<PSObject>;
            if (psDataCollection != null)
            {
                var psObject = psDataCollection[e.Index];

                // Apply formatter prior to output
                var formatter = new PSObjectFormatter();
                string output = formatter.Format(psObject);

                _runnerConfiguration.OnObjectOutput(output);
            }
        }

        private void OnVerbose(object sender, DataAddedEventArgs e)
        {
            WriteOutput(sender, e);
        }

        private void OnProgress(object sender, DataAddedEventArgs e)
        {
            var psDataCollection = sender as PSDataCollection<ProgressRecord>;
            if (psDataCollection != null)
            {
                var currentProgress = psDataCollection[e.Index];
                if (currentProgress.PercentComplete >= 0)
                {
                    _runnerConfiguration.OnProgress(currentProgress.PercentComplete);
                }
            }
        }

        private void OnWarning(object sender, DataAddedEventArgs e)
        {
            WriteOutput(sender, e);
        }

        private void OnDebug(object sender, DataAddedEventArgs e)
        {
            WriteOutput(sender, e);
        }

        private void OnError(object sender, DataAddedEventArgs e)
        {
            _errorCount++;
            WriteError(sender ,e);
        }

        private void WriteOutput(object sender, DataAddedEventArgs e)
        {
            if (_runnerConfiguration.OnOutput == null)
            {
                return;
            }

            var psDataCollection = sender as PSDataCollection<PSObject>;
            if (psDataCollection != null)
            {
                PSObject psObject = psDataCollection[e.Index];

                _runnerConfiguration.OnOutput(psObject.ToString());
            }
        }

        private void WriteError(object sender, DataAddedEventArgs e)
        {
            if (_runnerConfiguration.OnError == null)
            {
                return;
            }

            var psErrorCollection = sender as PSDataCollection<ErrorRecord>;
            if (psErrorCollection != null)
            {
                ErrorRecord error = psErrorCollection[e.Index];
                _runnerConfiguration.OnError(error.Exception.ToString());
            }
        }
    }
}
