using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ParameterizedThreadStart_Test.Run();
            Timer_Test.Run();
            System.Console.ReadKey();
        }
    }
}
