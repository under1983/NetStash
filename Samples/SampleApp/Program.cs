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
            NetStashLog log = new NetStashLog("172.168.2.102", 5356, System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.FileVersion, "hola");

            log.Error("Testing", System.Reflection.MethodBase.GetCurrentMethod());

            Thread.Sleep(500);

            log.Stop();
        }
    }
}
