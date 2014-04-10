using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Test
{
    public delegate int DelegateFunc(string s);//定义可注册的函数形式
    public class A
    {
        event DelegateFunc delegateHandler;
        public void add(DelegateFunc d)
        {
            delegateHandler += d;
        }
        public void remove(DelegateFunc d)
        {
            delegateHandler -= d;
        }
        public void Notify()
        {
            delegateHandler("all"); // 将所有注册了的函数都调用一边（带有对象指针，实际上是某一个对象的成员函数）
        }
    }

    public class B
    {
        public int funcB(string s) // 只要满足函数形式即可，不需要继承自某一个基类
        {
            System.Console.WriteLine("B" + s);
            return 0;
        }
    }
    public class C
    {
        public int funcC(string s)
        {
            System.Console.WriteLine("C" + s);
            return 0;
        }
    }

    public class DelegateTest
    {
        public void call()
        {
            A a = new A();
            B b = new B();
            C c = new C();
            DelegateFunc dc = new DelegateFunc(c.funcC);
            a.add(new DelegateFunc(b.funcB)); // 要封装成一个delegate对象，这个对象就包含了对象ref和成员函数指针
            a.add(dc);
            a.Notify();
            System.Console.WriteLine("----------");
            a.remove(dc);
            a.Notify();
        }
    }
}
