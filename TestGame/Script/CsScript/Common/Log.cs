using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Common.Log;

namespace TestGameServer.Common
{
    static public class GameLog
    {
        public static void Info(string message, params object[] args)
        {
            TraceLog.WriteInfo(message, args);
            System.Console.WriteLine(message, args);
        }
        public static void Error(string message, params object[] args)
        {
            TraceLog.WriteError(message, args);
            System.Console.WriteLine(message, args);
        }
        public static void Warn(string message, params object[] args)
        {
            TraceLog.WriteWarn(message, args);
            System.Console.WriteLine(message, args);
        }
    }
}
