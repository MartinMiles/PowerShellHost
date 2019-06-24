# PowerShellHost
A lightweight and extendable PowerShell host to be used with your GUI applications


## About

The library helps you executing custom PowerShell scripts from within your console or GUI-based application. 
It supports an objective nature of PowerShell and allows you to intercept not just primary output, but also handle numerous stream, such as:
* Progress
* Error
* Verbose
* Debug
* Warning


### Usage

Once GitHub Package Registry becomes publically available, this library will be shipped throught it for simplicity.
Alternatively, the Release version of the libraris are located ar `Release` folder, compiled with .NET Framework 4.7.


For sample of usages, please refer to `MinimalExecutionTest`. Some very basic call looks as below:


```
var outputLines = new List<string>();

var parameters = new Dictionary<string, string>
{
    { "ServerInstance", "." },
    { "Username", "sa" },
    { "Password", "SA_PASSWORD" }
};

var conf = new RunnerConfiguration
{
    IsRemote = false,
    OnObjectOutput = line => { outputLines.Add(line); },
    Parameters = parameters
};

var runner = new PowerShellRunner(conf);
runner.Run("ScriptFilePath");
```


### Formatters

Formatters are used in order to display object-based output stream. You may define own formatters to support any custom types, that can be implemented and referenced from `PSObjectFormatter.CreateFormatter()` method.