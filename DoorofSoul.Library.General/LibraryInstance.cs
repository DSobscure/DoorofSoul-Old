using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsgPack.Serialization;
using DoorofSoul.Library.General.LightComponents.HexagramInterfaces;

namespace DoorofSoul.Library.General
{
    public static class LibraryInstance
    {
        static Action<object> errorFunction;
        static Action<string, object[]> errorFormatFunction;
        public static KnowledgeInterface KnowledgeInterface { get; private set; }
        public static ElementInterface ElementInterface { get; private set; }
        public static NatureInterface NatureInterface { get; private set; }

        public static void Initial(Action<object> errorFunction, Action<string, object[]> errorFormatFunction, LibraryHexagramInterface hexagramInterface)
        {
            LibraryInstance.errorFunction = errorFunction;
            LibraryInstance.errorFormatFunction = errorFormatFunction;
            KnowledgeInterface = hexagramInterface?.KnowledgeInterface;
            ElementInterface = hexagramInterface?.ElementInterface;
            NatureInterface = hexagramInterface?.NatureInterface;
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
