using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataCodes;

namespace DoorofSoul.Library.General.Events.Handlers.World.InformData
{
    public class InformSceneHandler : InformDataHandler
    {
        public InformSceneHandler(General.World world) : base(world)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            throw new NotImplementedException();
        }

        public override bool Handle(WorldInformDataCode informCode, ErrorCode returnCode, Dictionary<byte, object> parameter)
        {
            return base.Handle(informCode, returnCode, parameter);
        }
    }
}
