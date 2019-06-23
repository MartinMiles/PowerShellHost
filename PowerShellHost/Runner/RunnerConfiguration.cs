using System;
using System.Collections.Generic;

namespace PowerShellHost.Runner
{
  public class RunnerConfiguration
  {
    public bool IsRemote { get; set; }
    
    public string RemoteHostName { get; set; }

    public Dictionary<string, string> Parameters { get; set; }

    public Action<string> OnOutput { get; set; }

    public Action<string> OnObjectOutput { get; set; }

    public Action<int> OnProgress { get; set; }

    public Action<string> OnError { get; set; }
  }
}
