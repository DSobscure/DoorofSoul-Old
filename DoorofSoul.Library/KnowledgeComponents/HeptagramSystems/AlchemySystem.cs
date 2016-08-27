using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.SkillParameters.AlchemySystem;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.KnowledgeComponents.HeptagramSystems
{
    public class AlchemySystem : HeptagramSystem
    {
        public override HeptagramSystemTypeCode SystemTypeCode { get { return HeptagramSystemTypeCode.Alchemy; } }

        public override bool OperateSkill(Soul user, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage)
        {
            if (base.OperateSkill(user, agent, skillInfo, skillParameters, out skillResponseParameters, out errorCode, out debugMessage))
            {
                try
                {
                    if ((SkillIDCode)skillInfo.Skill.SkillID == SkillIDCode.CreateItemEntity)
                    {
                        int itemID = (int)skillParameters[(byte)CreateItemEntityParameterCode.ItemID];
                        int itemCount = (int)skillParameters[(byte)CreateItemEntityParameterCode.SceneID];
                        int sceneID = (int)skillParameters[(byte)CreateItemEntityParameterCode.ItemID];
                        DSVector3 itemEntityPosition = (DSVector3)skillParameters[(byte)CreateItemEntityParameterCode.ItemEntityPosition];
                        if (Hexagram.Nature.SceneManager.ContainsScene(sceneID))
                        {
                            Scene scene = Hexagram.Nature.SceneManager.FindScene(sceneID);
                            for (int i = 0; i < itemCount; i++)
                            {
                                scene.ItemEntityManager.CreateItemEntity(itemID, itemEntityPosition);
                            }
                            skillResponseParameters = new Dictionary<byte, object>();
                            debugMessage = "";
                            return true;
                        }
                        else
                        {
                            debugMessage = string.Format("AlchemySystem OperateSkill Scene Not Exist SceneID: {0}", sceneID);
                            return false;
                        }
                    }
                    else
                    {
                        skillResponseParameters = new Dictionary<byte, object>();
                        debugMessage = string.Format("Skill Not Exist SkillID: {0}", skillInfo.Skill.SkillID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    Hexagram.Log.ErrorFormat("AlchemySystem OperateSkill Invalid Cast SkillID: {0}", skillInfo.Skill.SkillID);
                    Hexagram.Log.Error(ex.Message);
                    Hexagram.Log.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    Hexagram.Log.Error(ex.Message);
                    Hexagram.Log.Error(ex.StackTrace);
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
