using System;
using System.Collections.Generic;
using DoorofSoul.Protocol;

namespace DoorofSoul.Library.General.SceneElements
{
    public struct MessageInformation
    {
        public MessageType messageType;
        public MessageSourceType messageSourceType;
        public string sourceName;
        public string message;

    }
    public class MessageLog
    {
        #region events
        private event Action<MessageInformation> onReceiveNewMessage;
        public event Action<MessageInformation> OnReceiveNewMessage { add { onReceiveNewMessage += value; } remove { onReceiveNewMessage -= value; } }
        #endregion

        public List<MessageInformation> Messages { get; protected set; }

        public MessageLog()
        {
            Messages = new List<MessageInformation>();
        }

        public void ReceiveNewMessage(MessageInformation newMessageInformation)
        {
            Messages.Add(newMessageInformation);
            onReceiveNewMessage?.Invoke(newMessageInformation);
        }
    }
}
