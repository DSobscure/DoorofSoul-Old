using DoorofSoul.Library.General.ThroneComponents;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.UITestScripts
{
    public class SoulsPanelTest : MonoBehaviour
    {

        // Use this for initialization
        void Awake()
        {
            Global.Global.Player.LoadPlayer(1, "測試帳號", "測試暱稱", DoorofSoul.Protocol.Language.SupportLauguages.Chinese_Traditional, 1);
            Global.Global.Player.ActiveAnswer(new Answer(1, 3, Global.Global.Player));
        }
    }
}
