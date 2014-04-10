using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCSharp
{
    class ParameterizedThreadStart_Test
    {
        class A
        {
            public int a;
            public override string ToString() { return "A:" + a.ToString(); }
        }
        static public void F1() { System.Console.WriteLine("F1"); }
        static public void F2(object a) { System.Console.WriteLine("F2" + a.ToString()); }
        public void F3() { System.Console.WriteLine("F3"); }
        public void F4(object a) { System.Console.WriteLine("F4" + a.ToString()); }

        public static void Run()
        {
            Thread t1 = new Thread(F1);
            Thread t2 = new Thread(F2);
            Thread t3 = new Thread(new ParameterizedThreadStart_Test().F3);
            Thread t4 = new Thread(new ParameterizedThreadStart_Test().F4);
            t1.Start();
            t2.Start(new A() { a = 2 });
            t3.Start();
            t4.Start(new A() { a = 3 });

            Thread t5 = new Thread(() => { System.Console.WriteLine("F5"); });
            t5.Start();
            t5.Join();

            Thread t6 = new Thread((a) => { System.Console.WriteLine("F5"+a.ToString()); });
            t6.Start(new A() { a = 6 });
            t6.Join();
           
            System.Console.ReadKey();
        }
    }
}
