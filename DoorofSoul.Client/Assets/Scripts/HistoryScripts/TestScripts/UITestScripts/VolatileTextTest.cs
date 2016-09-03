using UnityEngine;
using DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.EffectScripts;

namespace Assets.Scripts.HistoryScripts.TestScripts.UITestScripts
{
    public class VolatileTextTest : MonoBehaviour
    {
        [SerializeField]
        private VolatileText text;

        void Start()
        {
            text.Initial(0.5f, "123");
        }
    }
}
