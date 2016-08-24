using DoorofSoul.Client.HelpFunctions;
using DoorofSoul.Client.Protocol;
using DoorofSoul.Client.Protocol.Language;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.ExtraPanelScripts
{
    public class SkillPanel : MonoBehaviour
    {
        private SkillLibrary skillLibrary;
        public SupportLauguages UsingLanguage { get { return skillLibrary.Owner.UsingLanguage; } }
        private Dictionary<int, SkillInfoBlock> skillInfoBlockDictionary;

        private Text titleText;
        private Button closeButton;

        private Text systemLabelText;
        private Dropdown heptagramSystemSelectDropdown;
        private Text viewModeLabelText;
        private Dropdown skillInfosViewModeSelectDropdown;

        private ScrollRect skillInfosScrollView;
        private RectTransform skillInfosScrollViewContent;

        [SerializeField]
        private SkillInfoBlock skillInfoBlockPrefab;

        public void Initial(SkillLibrary skillLibrary)
        {
            this.skillLibrary = skillLibrary;
            skillInfoBlockDictionary = new Dictionary<int, SkillInfoBlock>();
            
            titleText = transform.FindChild("TitleBar").FindChild("TitleText").GetComponent<Text>();
            closeButton = transform.FindChild("TitleBar").FindChild("CloseButton").GetComponent<Button>();
            systemLabelText = transform.FindChild("SystemLabelText").GetComponent<Text>();
            heptagramSystemSelectDropdown = transform.FindChild("HeptagramSystemSelectDropdown").GetComponent<Dropdown>();
            viewModeLabelText = transform.FindChild("ViewModeLabelText").GetComponent<Text>();
            skillInfosViewModeSelectDropdown = transform.FindChild("SkillInfosViewModeSelectDropdown").GetComponent<Dropdown>();
            skillInfosScrollView = transform.FindChild("SkillInfosScrollView").GetComponent<ScrollRect>();
            skillInfosScrollViewContent = skillInfosScrollView.transform.FindChild("Viewport").FindChild("Content").GetComponent<RectTransform>();

            titleText.text = UILanguageSeletor.Instance[UsingLanguage]["SkillLibrary"];
            closeButton.onClick.AddListener(() => Destroy(gameObject)); ;
            systemLabelText.text = UILanguageSeletor.Instance[UsingLanguage]["HeptagramSystem"];
            heptagramSystemSelectDropdown.AddOptions(skillLibrary.Systems.Select(x => new Dropdown.OptionData(UILanguageSeletor.Instance[UsingLanguage][x.ToString()])).ToList());
            viewModeLabelText.text = UILanguageSeletor.Instance[UsingLanguage]["ViewMode"];

            heptagramSystemSelectDropdown.onValueChanged.AddListener((value) => ShowSkillInfos());
            skillInfosViewModeSelectDropdown.onValueChanged.AddListener((value) => ShowSkillInfos());


            List<Dropdown.OptionData> viewModeOptions = new List<Dropdown.OptionData>();
            foreach(string option in Enum.GetNames(typeof(SkillPanelViewMode)))
            {
                viewModeOptions.Add(new Dropdown.OptionData(UILanguageSeletor.Instance[UsingLanguage][option]));
            }
            skillInfosViewModeSelectDropdown.AddOptions(viewModeOptions);

            ShowSkillInfos();
        }

        public void ShowSkillInfos()
        {
            skillInfoBlockDictionary.Clear();
            skillInfosScrollViewContent.transform.ClearChild();
            if(heptagramSystemSelectDropdown.options.Count > 0)
            {
                HeptagramSystemTypeCode systemTypeCode = skillLibrary.Systems.ToArray()[heptagramSystemSelectDropdown.value];
                List<SkillInfo> skillInfos = skillLibrary.SkillInfos.Where(x => x.Skill.SystemTypeCode == systemTypeCode).ToList();
                SkillPanelViewMode viewMode = (SkillPanelViewMode)Enum.GetValues(typeof(SkillPanelViewMode)).GetValue(skillInfosViewModeSelectDropdown.value);
                switch (viewMode)
                {
                    case SkillPanelViewMode.List:
                        skillInfosScrollViewContent.sizeDelta = new Vector2(skillInfoBlockPrefab.Width, 10 + (skillInfoBlockPrefab.Height + 5) * skillInfos.Count);
                        skillInfosScrollViewContent.anchoredPosition = new Vector2(-skillInfoBlockPrefab.Width / 2, 0);
                        for (int i = 0; i < skillInfos.Count; i++)
                        {
                            if (!skillInfoBlockDictionary.ContainsKey(skillInfos[i].Skill.SkillID))
                            {
                                SkillInfoBlock skillInfoBlock = Instantiate(skillInfoBlockPrefab);
                                RectTransform blockRectTransform = skillInfoBlock.GetComponent<RectTransform>();
                                blockRectTransform.transform.SetParent(skillInfosScrollViewContent.transform);
                                blockRectTransform.localScale = Vector3.one;
                                blockRectTransform.anchorMin = new Vector2(0.5f, 1);
                                blockRectTransform.anchorMax = new Vector2(0.5f, 1);
                                blockRectTransform.pivot = new Vector2(0.5f, 0.5f);
                                float x = 0;
                                float y = (blockRectTransform.sizeDelta.y + 5) * (i + 1) - blockRectTransform.sizeDelta.y / 2;
                                blockRectTransform.anchoredPosition = new Vector2(x, -y);
                                skillInfoBlock.Initial(this, skillInfos[i]);
                                skillInfoBlockDictionary.Add(skillInfos[i].Skill.SkillID, skillInfoBlock);
                            }
                            else
                            {
                                skillInfoBlockDictionary[skillInfos[i].Skill.SkillID].ExtendPitch(skillInfos[i]);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void Close()
        {
            closeButton.onClick.Invoke();
        }
    }
}
