using System;
using System.Diagnostics;
using System.IO;

namespace PowerShellHost.Tests
{
    public class BaseTest
    {
        public string ScriptDirectory
        {
            get
            {
                var info = new DirectoryInfo(Environment.CurrentDirectory);
                return $"{info.Parent.Parent.FullName}\\TestScripts";
            }
        }

        public string ScriptFile
        {
            get
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                var currentMethodName = sf.GetMethod().Name;

                string scriptPath = $"{ScriptDirectory}\\{currentMethodName}.ps1";
                if (!File.Exists(scriptPath))
                {
                    throw new FileNotFoundException("Please ensure test script exists in directory: " + scriptPath);
                }

                return scriptPath;
            }
        }
    }
}