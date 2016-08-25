using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsgPack.Serialization;
using DoorofSoul.Library.General.LightComponents;

namespace DoorofSoul.Library.General
{
    public static class LibraryInstance
    {
        static Action<object> errorFunction;
        static Action<string, object[]> errorFormatFunction;
        public static KnowledgeInterface KnowledgeInterface { get; private set; }

        public static void Initial(Action<object> errorFunction, Action<string, object[]> errorFormatFunction, KnowledgeInterface knowledgeInterface)
        {
            LibraryInstance.errorFunction = errorFunction;
            LibraryInstance.errorFormatFunction = errorFormatFunction;
            KnowledgeInterface = knowledgeInterface;
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
