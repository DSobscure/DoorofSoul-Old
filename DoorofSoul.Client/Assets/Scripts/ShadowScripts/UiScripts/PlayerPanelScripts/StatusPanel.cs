using System;
using System.Collections.Generic;
using DoorofSoul.Protocol;
using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Client.HelpFunctions;

namespace DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.PlayerPanelScripts
{
    public class StatusPanel : MonoBehaviour
    {
        [SerializeField]
        private Button statusEffectIconPrefab;

        private ContainerStatusEffectManager containerStatusEffectManager;
        private Dictionary<int, Button> containerStatusEffectIconDictionary;

        public StatusPanel()
        {
            containerStatusEffectIconDictionary = new Dictionary<int, Button>();
        }

        public void BindContainerStatusEffectManager(ContainerStatusEffectManager containerStatusEffectManager)
        {
            if (containerStatusEffectManager == null)
            {
                
                this.containerStatusEffectManager = containerStatusEffectManager;
                containerStatusEffectManager.OnContainerStatusEffectInfoChange += OnContainerStatusEffectInfoChange;
                OnContainerStatusEffectInfoChange(null, DataChangeTypeCode.Initial);
            }
            else
            {
                OnContainerStatusEffectInfoChange(null, DataChangeTypeCode.ClearAll);
                containerStatusEffectManager.OnContainerStatusEffectInfoChange -= OnContainerStatusEffectInfoChange;

                this.containerStatusEffectManager = containerStatusEffectManager;
                containerStatusEffectManager.OnContainerStatusEffectInfoChange += OnContainerStatusEffectInfoChange;
                OnContainerStatusEffectInfoChange(null, DataChangeTypeCode.Initial);
            }
        }

        private void OnContainerStatusEffectInfoChange(ContainerStatusEffectInfo info, DataChangeTypeCode changeTypeCode)
        {
            switch(changeTypeCode)
            {
                case DataChangeTypeCode.Load:
                    if (!containerStatusEffectIconDictionary.ContainsKey(info.ContainerStatusEffectInfoID))
                    {
                        Button newIcon = Instantiate(statusEffectIconPrefab);
                        RectTransform blockRectTransform = newIcon.GetComponent<RectTransform>();
                        blockRectTransform.transform.SetParent(transform);
                        blockRectTransform.localScale = Vector3.one;
                        blockRectTransform.anchorMin = new Vector2(1, 0.5f);
                        blockRectTransform.anchorMax = new Vector2(1, 0.5f);
                        blockRectTransform.pivot = new Vector2(0.5f, 0.5f);
                        float x = (blockRectTransform.sizeDelta.y + 5) * (containerStatusEffectIconDictionary.Count + 1) - (blockRectTransform.sizeDelta.y + 5) / 2;
                        float y = 0;
                        blockRectTransform.anchoredPosition = new Vector2(-x, y);
                        containerStatusEffectIconDictionary.Add(info.ContainerStatusEffectInfoID, newIcon);
                        if(info.StatusEffect.StatusEffectID == 1)
                        {
                            Global.Global.IsObserver = true;
                        }
                    }
                    break;
                case DataChangeTypeCode.Unload:
                    if (containerStatusEffectIconDictionary.ContainsKey(info.ContainerStatusEffectInfoID))
                    {
                        Destroy(containerStatusEffectIconDictionary[info.ContainerStatusEffectInfoID].gameObject);
                        containerStatusEffectIconDictionary.Remove(info.ContainerStatusEffectInfoID);
                        if (info.StatusEffect.StatusEffectID == 1)
                        {
                            Global.Global.IsObserver = false;
                        }
                    }
                    break;
                case DataChangeTypeCode.Update:
                    if (containerStatusEffectIconDictionary.ContainsKey(info.ContainerStatusEffectInfoID))
                    {

                    }
                    break;
                case DataChangeTypeCode.Initial:
                    foreach(ContainerStatusEffectInfo initialInfo in containerStatusEffectManager.StatusEffectInfos)
                    {
                        if (!containerStatusEffectIconDictionary.ContainsKey(initialInfo.ContainerStatusEffectInfoID))
                        {
                            Button newIcon = Instantiate(statusEffectIconPrefab);
                            RectTransform blockRectTransform = newIcon.GetComponent<RectTransform>();
                            blockRectTransform.transform.SetParent(transform);
                            blockRectTransform.localScale = Vector3.one;
                            blockRectTransform.anchorMin = new Vector2(1, 0.5f);
                            blockRectTransform.anchorMax = new Vector2(1, 0.5f);
                            blockRectTransform.pivot = new Vector2(0.5f, 0.5f);
                            float x = (blockRectTransform.sizeDelta.y + 5) * (containerStatusEffectIconDictionary.Count + 1) - (blockRectTransform.sizeDelta.y + 5) / 2;
                            float y = 0;
                            blockRectTransform.anchoredPosition = new Vector2(-x, y);
                            containerStatusEffectIconDictionary.Add(info.ContainerStatusEffectInfoID, newIcon);
                        }
                    }
                    break;
                case DataChangeTypeCode.ClearAll:
                    transform.ClearChildren();
                    containerStatusEffectIconDictionary.Clear();
                    break;
                default:
                    break;
            }
        }
    }
}
