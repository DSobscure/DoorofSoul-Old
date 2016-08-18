using System;
using System.Collections.Generic;
using DoorofSoul.Protocol;

namespace DoorofSoul.Library.General.NatureComponents.SceneElements
{
    public struct MessageInformation
    {
        public MessageTypeCode messageType;
        public MessageSourceTypeCode messageSourceType;
        public string sourceName;
        public string message;

    }
    public class MessageLog
    {
        #region events
        private event Action<MessageInformation> onReceiveNewMessage;
        public event Action<MessageInformation> OnReceiveNewMessage { add { onReceiveNewMessage += value; } remove { onReceiveNewMessage -= value; } }
        private event Action<List<MessageInformation>> onMessageChange;
        public event Action<List<MessageInformation>> OnMessageChange { add { onMessageChange += value; } remove { onMessageChange -= value; } }
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
            if(Messages.Count > 500)
            {
                Messages.RemoveRange(0, 50);
            }
            onMessageChange?.Invoke(Messages);
        }
    }
}
