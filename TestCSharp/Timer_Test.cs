using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCSharp
{
    class Timer_Test
    {
        public static void Run()
        {
            System.Timers.Timer tr = new System.Timers.Timer(1000);
            tr.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => { System.Console.WriteLine("kkk"); };
            tr.Disposed += (sender, e) => { System.Console.WriteLine("end"); };
            tr.Enabled = true;

            Thread.Sleep(5000);
            tr.Dispose();
        }
    }
}
