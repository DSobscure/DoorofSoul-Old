using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public static class LibraryLog
    {
        static Action<object> errorFunction;
        static Action<string, object[]> errorFormatFunction;

        public static void Initial(Action<object> errorFunction, Action<string, object[]> errorFormatFunction)
        {
            LibraryLog.errorFunction = errorFunction;
            LibraryLog.errorFormatFunction = errorFormatFunction;
        }
        public static void ErrorFormat(string message, params object[] parameters)
        {
            errorFormatFunction(message, parameters);
        }
        public static void Error(object message)
        {
            errorFunction(message);
        }
    }
}
