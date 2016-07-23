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

        public override bool Handle(FetchDataCode fetchCode, Dictionary<byte, object> parameter)
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
                                { (byte)InformSceneEntityEnterParameterCode.EntityID, entity.EntityID },
                                { (byte)InformSceneEntityEnterParameterCode.EntityName, entity.EntityName },
                                { (byte)InformSceneEntityEnterParameterCode.LocatedSceneID, entity.LocatedSceneID },
                                { (byte)InformSceneEntityEnterParameterCode.PositionX, entity.Position.x },
                                { (byte)InformSceneEntityEnterParameterCode.PositionY, entity.Position.y },
                                { (byte)InformSceneEntityEnterParameterCode.PositionZ, entity.Position.z },
                                { (byte)InformSceneEntityEnterParameterCode.RotationX, entity.Rotation.x },
                                { (byte)InformSceneEntityEnterParameterCode.RotationY, entity.Rotation.y },
                                { (byte)InformSceneEntityEnterParameterCode.RotationZ, entity.Rotation.z },
                                { (byte)InformSceneEntityEnterParameterCode.ScaleX, entity.Scale.x },
                                { (byte)InformSceneEntityEnterParameterCode.ScaleY, entity.Scale.y },
                                { (byte)InformSceneEntityEnterParameterCode.ScaleZ, entity.Scale.z },
                                { (byte)InformSceneEntityEnterParameterCode.VelocityX, entity.Velocity.x },
                                { (byte)InformSceneEntityEnterParameterCode.VelocityY, entity.Velocity.y },
                                { (byte)InformSceneEntityEnterParameterCode.VelocityZ, entity.Velocity.z },
                                { (byte)InformSceneEntityEnterParameterCode.MaxVelocityX, entity.MaxVelocity.x },
                                { (byte)InformSceneEntityEnterParameterCode.MaxVelocityY, entity.MaxVelocity.y },
                                { (byte)InformSceneEntityEnterParameterCode.MaxVelocityZ, entity.MaxVelocity.z },
                                { (byte)InformSceneEntityEnterParameterCode.AngularVelocityX, entity.AngularVelocity.x },
                                { (byte)InformSceneEntityEnterParameterCode.AngularVelocityY, entity.AngularVelocity.y },
                                { (byte)InformSceneEntityEnterParameterCode.AngularVelocityZ, entity.AngularVelocity.z },
                                { (byte)InformSceneEntityEnterParameterCode.MaxAngularVelocityX, entity.MaxAngularVelocity.x },
                                { (byte)InformSceneEntityEnterParameterCode.MaxAngularVelocityY, entity.MaxAngularVelocity.y },
                                { (byte)InformSceneEntityEnterParameterCode.MaxAngularVelocityZ, entity.MaxAngularVelocity.z },
                                { (byte)InformSceneEntityEnterParameterCode.Mass, entity.Mass },
                            };
                            SendEvent((byte)InformDataCode.SceneEntityEnter, result);
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
