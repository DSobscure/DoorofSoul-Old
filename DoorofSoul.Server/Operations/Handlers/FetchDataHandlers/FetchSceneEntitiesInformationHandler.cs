using DoorofSoul.Library;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers.FetchDataHandlers
{
    public class FetchSceneEntitiesInformationHandler : FetchDataHandler
    {
        public FetchSceneEntitiesInformationHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = string.Format("fetch scene entities information has {0} parameters!", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    int sceneID = (int)parameter[(byte)FetchSceneEntitiesInformationParameterCode.SceneID];
                    if (Hexagram.Instance.Nature.ContainsScene(sceneID))
                    {
                        Scene scene = Hexagram.Instance.Nature.FindScene(sceneID);
                        foreach (Entity entity in scene.Entities)
                        {
                            var result = new Dictionary<byte, object>
                            {
                                { (byte)InformEntityEnterParameterCode.EntityID, entity.EntityID },
                                { (byte)InformEntityEnterParameterCode.EntityName, entity.EntityName },
                                { (byte)InformEntityEnterParameterCode.LocatedSceneID, entity.LocatedSceneID },
                                { (byte)InformEntityEnterParameterCode.PositionX, entity.Position.x },
                                { (byte)InformEntityEnterParameterCode.PositionY, entity.Position.y },
                                { (byte)InformEntityEnterParameterCode.PositionZ, entity.Position.z },
                                { (byte)InformEntityEnterParameterCode.RotationX, entity.Rotation.x },
                                { (byte)InformEntityEnterParameterCode.RotationY, entity.Rotation.y },
                                { (byte)InformEntityEnterParameterCode.RotationZ, entity.Rotation.z },
                                { (byte)InformEntityEnterParameterCode.ScaleX, entity.Scale.x },
                                { (byte)InformEntityEnterParameterCode.ScaleY, entity.Scale.y },
                                { (byte)InformEntityEnterParameterCode.ScaleZ, entity.Scale.z },
                                { (byte)InformEntityEnterParameterCode.VelocityX, entity.Velocity.x },
                                { (byte)InformEntityEnterParameterCode.VelocityY, entity.Velocity.y },
                                { (byte)InformEntityEnterParameterCode.VelocityZ, entity.Velocity.z },
                                { (byte)InformEntityEnterParameterCode.MaxVelocityX, entity.MaxVelocity.x },
                                { (byte)InformEntityEnterParameterCode.MaxVelocityY, entity.MaxVelocity.y },
                                { (byte)InformEntityEnterParameterCode.MaxVelocityZ, entity.MaxVelocity.z },
                                { (byte)InformEntityEnterParameterCode.AngularVelocityX, entity.AngularVelocity.x },
                                { (byte)InformEntityEnterParameterCode.AngularVelocityY, entity.AngularVelocity.y },
                                { (byte)InformEntityEnterParameterCode.AngularVelocityZ, entity.AngularVelocity.z },
                                { (byte)InformEntityEnterParameterCode.MaxAngularVelocityX, entity.MaxAngularVelocity.x },
                                { (byte)InformEntityEnterParameterCode.MaxAngularVelocityY, entity.MaxAngularVelocity.y },
                                { (byte)InformEntityEnterParameterCode.MaxAngularVelocityZ, entity.MaxAngularVelocity.z },
                                { (byte)InformEntityEnterParameterCode.Mass, entity.Mass },
                            };
                            SendEvent((byte)PlayerInformDataCode.SceneEntityEnter, result);
                        }
                        return true;
                    }
                    else
                    {
                        Application.Log.ErrorFormat("FetchSceneEntitiesInformation Scene Not Exist!");
                        SendError((byte)fetchCode, ErrorCode.NotExist, "Scene not exist", null);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    Application.Log.ErrorFormat("FetchSceneEntitiesInformation Invalid Cast!");
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
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
