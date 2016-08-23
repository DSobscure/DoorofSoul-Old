using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Client.Scripts.UiScripts.PlayerPanelScripts
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
                foreach (ContainerStatusEffectInfo info in containerStatusEffectManager.StatusEffectInfos)
                {
                    OnContainerStatusEffectInfoChange(info, true);
                }
            }
            else
            {
                foreach (ContainerStatusEffectInfo info in containerStatusEffectManager.StatusEffectInfos)
                {
                    OnContainerStatusEffectInfoChange(info, false);
                }
                containerStatusEffectManager.OnContainerStatusEffectInfoChange -= OnContainerStatusEffectInfoChange;

                this.containerStatusEffectManager = containerStatusEffectManager;
                containerStatusEffectManager.OnContainerStatusEffectInfoChange += OnContainerStatusEffectInfoChange;
                foreach (ContainerStatusEffectInfo info in containerStatusEffectManager.StatusEffectInfos)
                {
                    OnContainerStatusEffectInfoChange(info, true);
                }
            }
        }

        private void OnContainerStatusEffectInfoChange(ContainerStatusEffectInfo info, bool isLoad)
        {
            if(isLoad)
            {
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
                }
            }
            else
            {
                if(containerStatusEffectIconDictionary.ContainsKey(info.ContainerStatusEffectInfoID))
                {
                    Destroy(containerStatusEffectIconDictionary[info.ContainerStatusEffectInfoID].gameObject);
                    containerStatusEffectIconDictionary.Remove(info.ContainerStatusEffectInfoID);
                }
            }
        }
    }
}
