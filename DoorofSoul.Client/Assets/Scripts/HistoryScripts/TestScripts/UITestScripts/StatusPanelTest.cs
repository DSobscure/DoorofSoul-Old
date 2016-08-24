using DoorofSoul.Client.Library.General;
using DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.PlayerPanelScripts;
using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Library.General.ThroneComponents.SoulElements;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.UITestScripts
{
    class StatusPanelTest : MonoBehaviour
    {
        [SerializeField]
        private PlayerPanel playerPanel;
        private ContainerStatusEffectManager manager;

        void Start()
        {
            Scene scene = new Scene(1, "TestScene", 1);
            DoorofSoul.Library.General.Player player = new DoorofSoul.Library.General.Player(new ClientPlayerCommunicationInterface(null, null, null));
            player.LoadPlayer(1, "", "", SupportLauguages.Chinese_Traditional, 1);
            Soul soul = new Soul(1, new Answer(1, 1, player), "TestSoul", SoulAttributes.GetDefaultAttribute(DoorofSoul.Protocol.SoulKernelTypeCode.Creation));
            Container container = new Container(1, 1, "TestContainer", ContainerAttributes.GetDefaultAttribute());
            container.LinkSoul(soul);
            container.ContainerStatusEffectManager.InitialStatusEffectInfos(new List<ContainerStatusEffectInfo>
            {
                new ContainerStatusEffectInfo(1, 1, new StatusEffect(1, "測試1", 0), 0, DateTime.Now),
                new ContainerStatusEffectInfo(2, 1, new StatusEffect(2, "測試2", 10), 10, DateTime.Now),
                new ContainerStatusEffectInfo(3, 1, new StatusEffect(3, "測試3", -1), -1, DateTime.Now)
            });
            manager = container.ContainerStatusEffectManager;
            playerPanel.Initial(scene, soul, container);
            
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                manager.LoadStatusEffectInfo(new ContainerStatusEffectInfo(4, 1, new StatusEffect(4, "測試4", 5), 5, DateTime.Now));
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                manager.UnloadStatusEffectInfo(new ContainerStatusEffectInfo(4, 1, new StatusEffect(4, "測試4", 5), 5, DateTime.Now));
            }
        }
    }
}
