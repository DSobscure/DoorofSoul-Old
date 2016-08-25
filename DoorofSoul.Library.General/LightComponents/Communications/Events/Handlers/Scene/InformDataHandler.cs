﻿using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene
{
    public abstract class InformDataHandler
    {
        protected NatureComponents.Scene scene;
        protected int correctParameterCount;

        protected InformDataHandler(NatureComponents.Scene scene, int correctParameterCount)
        {
            this.scene = scene;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("Scene InformData Parameter Error On {0} SceneID: {1} Debug Message: {2}", informCode, scene.SceneID, debugMessage);
                return false;
            }
        }
        internal virtual bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                debugMessage = string.Format("Parameter Count: {0} Should be {1}", parameters.Count, correctParameterCount);
                return false;
            }
            else
            {
                debugMessage = "";
                return true;
            }
        }
    }
}