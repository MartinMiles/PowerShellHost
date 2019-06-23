using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerShellHost.Runner;

namespace PowerShellHost.Tests
{
    [TestClass]
    public class MinimalExecutionTest : BaseTest
    {
        [TestMethod]
        public void TestFormatter()
        {
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
                OnObjectOutput =  line => { outputLines.Add(line); },
                Parameters = parameters
            };

            var runner = new PowerShellRunner(conf);
            runner.Run(ScriptFile);

            Assert.AreEqual(outputLines.Count, 2);
            Assert.IsTrue(outputLines.ElementAt(0).Contains("Connecting"));
            Assert.IsTrue(outputLines.ElementAt(1).Contains("TimeOfQuery"));
        }

        [TestMethod]
        public void TestProgress()
        {
            var progress = new List<int>();

            var conf = new RunnerConfiguration
            {
                IsRemote = false,
                OnProgress = i => { progress.Add(i); },
            };

            var executor = new PowerShellRunner(conf);
            executor.Run(ScriptFile);

            Assert.AreEqual(progress.Count, 11);
            for(int i=0; i<=10; i++)
            {
                Assert.AreEqual(progress.ElementAt(i), i*10);
            }
        }
    }
}
