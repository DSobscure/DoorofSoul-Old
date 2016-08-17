using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Protocol;
using DoorofSoul.Client.Protocol.Language;
using DoorofSoul.Library.General.Skills;
using System.Text;
using System.Collections.Generic;

namespace DoorofSoul.Client.Scripts.UiScripts.ExtraPanelScripts
{
    public class SkillInfoBlock : MonoBehaviour
    {
        private SkillPanel skillPanel;
        private List<SkillInfo> skillInfos;

        private Button skillIconButton;
        private Text skillNameText;
        private Text skillLevelText;
        private Dropdown skillPitchDropdown;
        private Text skillBriefText;

        public float Width { get { return GetComponent<RectTransform>().sizeDelta.x; } }
        public float Height { get { return GetComponent<RectTransform>().sizeDelta.y; } }

        public void Initial(SkillPanel skillPanel, SkillInfo skillInfo)
        {
            skillInfos = new List<SkillInfo>();
            this.skillPanel = skillPanel;
            skillInfos.Add(skillInfo);
            skillIconButton = transform.FindChild("SkillIconButton").GetComponent<Button>();
            skillNameText = transform.FindChild("SkillNameText").GetComponent<Text>();
            skillLevelText = transform.FindChild("SkillLevelText").GetComponent<Text>();
            skillPitchDropdown = transform.FindChild("SkillPitchDropdown").GetComponent<Dropdown>();
            skillBriefText = transform.FindChild("SkillBrieftText").GetComponent<Text>();

            skillNameText.text = skillInfo.Skill.SkillName;
            skillLevelText.text = string.Format("LV.{0}", skillInfo.SkillLevel);
            skillPitchDropdown.AddOptions(new List<Dropdown.OptionData> { new Dropdown.OptionData(UILanguageSeletor.Instance[skillPanel.UsingLanguage][skillInfo.SkillPitch.ToString()]) });
            StringBuilder stringBuilder = new StringBuilder("");
            if(skillInfo.Skill.BasicLifePointCost != 0)
            {
                stringBuilder.AppendFormat("{0}: {1:N2}", UILanguageSeletor.Instance[skillPanel.UsingLanguage]["BasicLifePointCost"], skillInfo.Skill.BasicLifePointCost);
            }
            if (skillInfo.Skill.BasicEnergyPointCost != 0)
            {
                stringBuilder.AppendFormat("{0}: {1:N2}", UILanguageSeletor.Instance[skillPanel.UsingLanguage]["BasicEnergyPointCost"], skillInfo.Skill.BasicEnergyPointCost);
            }
            if (skillInfo.Skill.BasicCorePointCost != 0)
            {
                stringBuilder.AppendFormat("{0}: {1:N2}", UILanguageSeletor.Instance[skillPanel.UsingLanguage]["BasicCorePointCost"], skillInfo.Skill.BasicCorePointCost);
            }
            if (skillInfo.Skill.BasicSpiritPointCost != 0)
            {
                stringBuilder.AppendFormat("{0}: {1:N2}", UILanguageSeletor.Instance[skillPanel.UsingLanguage]["BasicSpiritPointCost"], skillInfo.Skill.BasicSpiritPointCost);
            }
            skillBriefText.text = stringBuilder.ToString();

            skillIconButton.onClick.AddListener(OperateSkill);
        }
        public void ExtendPitch(SkillInfo skillInfo)
        {
            skillInfos.Add(skillInfo);
            skillPitchDropdown.AddOptions(new List<Dropdown.OptionData> { new Dropdown.OptionData(UILanguageSeletor.Instance[skillPanel.UsingLanguage][skillInfo.SkillPitch.ToString()]) });
        }
        private void OperateSkill()
        {
            SkillInfo skillInfo = skillInfos[skillPitchDropdown.value];
            Global.Global.Seat.MainSoul.SoulOperationManager.OperateSkill(Global.Global.Seat.MainContainer.ContainerID, skillInfo.Skill.SystemTypeCode, skillInfo.SkillInfoID, new Dictionary<byte, object>());
        }
    }
}
