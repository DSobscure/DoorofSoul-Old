using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData
{
    internal class InformBroadcastMessageHandler : InformDataHandler
    {
        internal InformBroadcastMessageHandler(NatureComponents.Scene scene) : base(scene, 4)
        {
        }

        internal override bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    MessageTypeCode messageType = (MessageTypeCode)parameters[(byte)InformBroadcastMessageParameterCode.MessageType]; ;
                    MessageSourceTypeCode messageSourceType = (MessageSourceTypeCode)parameters[(byte)InformBroadcastMessageParameterCode.MessageSourceType];
                    string sourceName = (string)parameters[(byte)InformBroadcastMessageParameterCode.SourceName];
                    string message = (string)parameters[(byte)InformBroadcastMessageParameterCode.Message];
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
                    LibraryInstance.ErrorFormat("InformBroadcastMessage Event Parameter Cast Error");
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
