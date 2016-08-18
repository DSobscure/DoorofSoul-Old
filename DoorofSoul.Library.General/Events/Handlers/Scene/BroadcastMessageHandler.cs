using DoorofSoul.Library.General.SceneElements;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Scene
{
    internal class BroadcastMessageHandler : SceneEventHandler
    {
        internal BroadcastMessageHandler(General.Scene scene) : base(scene, 4)
        {
        }

        internal override bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    MessageTypeCode messageType = (MessageTypeCode)parameters[(byte)BroadcastMessageParameterCode.MessageType]; ;
                    MessageSourceTypeCode messageSourceType = (MessageSourceTypeCode)parameters[(byte)BroadcastMessageParameterCode.MessageSourceType];
                    string sourceName = (string)parameters[(byte)BroadcastMessageParameterCode.SourceName];
                    string message = (string)parameters[(byte)BroadcastMessageParameterCode.Message];
                    scene.MessageLog.ReceiveNewMessage(new MessageInformation
                    {
                        messageType = messageType,
                        messageSourceType = messageSourceType,
                        sourceName = sourceName,
                        message = message
                    });
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("BroadcastMessage Event Parameter Cast Error");
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
