using NetStash.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NetStashLog log = new NetStashLog("localhost", 1233, System.Diagnostics.Process.GetCurrentProcess().ProcessName, System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.FileVersion);

            log.Error("Testing", System.Reflection.MethodBase.GetCurrentMethod().Name);

            Thread.Sleep(50);

            log.Stop();
        }
    }
}
