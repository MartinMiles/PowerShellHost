namespace PowerShellHost.Runner
{
    public interface IPowerShellRunner
    {
        dynamic Run(string script);
    }
}