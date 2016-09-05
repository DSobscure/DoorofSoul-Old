using UnityEngine;
using DoorofSoul.Client.HelpFunctions;
using System.Linq;
using System;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.TestSceneScripts
{
    public class RankingPanel : MonoBehaviour
    {
        [SerializeField]
        RankingInfo infoPrefab;

        private RectTransform content;

        void Start()
        {
            content = transform.FindChild("ContentPanel").GetComponent<RectTransform>();
        }

        void Update()
        {
            if (Global.Global.Horizon.MainScene != null && Time.time % 2f > 1f)
            {
                content.ClearChildren();
                var containers = Global.Global.Horizon.MainScene.Containers.OrderBy(x => Convert.ToInt32(x.Attributes.LifePoint / 10) + x.ShooterAbilities.Damage + x.ShooterAbilities.MoveSpeed + x.ShooterAbilities.BulletSpeed + x.ShooterAbilities.Transparancy).Reverse().Take(10).ToArray();
                for(int i = 0; i < containers.Length; i++)
                {
                    var container = containers[i];
                    RankingInfo info = Instantiate(infoPrefab);
                    info.Initial(container.ContainerName, Convert.ToInt32(container.Attributes.LifePoint / 10) + container.ShooterAbilities.Damage + container.ShooterAbilities.MoveSpeed + container.ShooterAbilities.BulletSpeed + container.ShooterAbilities.Transparancy);
                    info.transform.SetParent(content);
                    RectTransform rect = info.GetComponent<RectTransform>();
                    rect.localScale = Vector3.one;
                    rect.anchorMin = new Vector2(0.5f, 1);
                    rect.anchorMax = new Vector2(0.5f, 1);
                    rect.pivot = new Vector2(0.5f, 0.5f);
                    float x = 0;
                    float y = 20 + i * 35;
                    rect.anchoredPosition = new Vector2(x, -y);
                }
            }
        }
    }
}
