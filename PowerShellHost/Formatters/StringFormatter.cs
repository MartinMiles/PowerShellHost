using System.Management.Automation;

namespace PowerShellHost.Formatters
{
    internal class StringFormatter :IFormatter
    {
        private readonly object _object;

        internal StringFormatter(object objectToFormat)
        {
            var baseObject = ((PSObject)objectToFormat).BaseObject;
            _object = baseObject;
        }
        public string Format()
        {
            return _object.ToString();
        }
    }
}
