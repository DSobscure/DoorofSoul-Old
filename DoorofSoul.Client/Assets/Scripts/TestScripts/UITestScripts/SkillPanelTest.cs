using DoorofSoul.Client.Library.General;
using DoorofSoul.Client.Scripts.UiScripts.ExtraPanelScripts;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.TestScripts.UITestScripts
{
    public class SkillPanelTest : MonoBehaviour
    {
        [SerializeField]
        public SkillPanel skillPanelPrefab;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                SkillPanel sk = Instantiate(skillPanelPrefab);
                sk.name = "SkillPanel";
                sk.transform.SetParent(GameObject.Find("Canvas").transform);
                RectTransform rectTransform = sk.GetComponent<RectTransform>();
                rectTransform.localScale = Vector3.one;
                rectTransform.localPosition = Vector2.zero;
                Player player = new Player(new ClientPlayerCommunicationInterface(null, null, null));
                player.LoadPlayer(1, "", "", SupportLauguages.Chinese_Traditional, 1);
                Soul soul = new Soul(1, new Answer(1, 1, player), null, null);
                SkillLibrary sl = new SkillLibrary(soul);
                Skill s = new Skill(1, HeptagramSystemTypeCode.Alchemy, SkillMediaTypeCode.Knowledge, "技能表練成");
                sl.LoadSkillInfos(new List<SkillInfo>
                {
                    new SkillInfo(1, 1, s, 1, SkillPitch.C),
                    new SkillInfo(2, 1, new Skill(2, HeptagramSystemTypeCode.Alchemy, SkillMediaTypeCode.Knowledge, "擴展技能表練成"), 1, SkillPitch.C),
                    new SkillInfo(3, 1, s, 1, SkillPitch.C_sharp),
                    new SkillInfo(4, 1, s, 1, SkillPitch.D),
                    new SkillInfo(5, 1, s, 1, SkillPitch.D_flat),
                    new SkillInfo(6, 1, new Skill(1, HeptagramSystemTypeCode.Belief, SkillMediaTypeCode.Throne, "上帝之眼"), 1, SkillPitch.C),
                    new SkillInfo(7, 1, new Skill(1, HeptagramSystemTypeCode.Chance, SkillMediaTypeCode.Light, "祝福"), 1, SkillPitch.C),
                    new SkillInfo(8, 1, new Skill(1, HeptagramSystemTypeCode.Demon, SkillMediaTypeCode.Light, "聖光"), 1, SkillPitch.C),
                });
                Debug.Log(sl.SkillInfos.ToList().Count);
                sk.Initial(sl);
            }
        }
    }
}
