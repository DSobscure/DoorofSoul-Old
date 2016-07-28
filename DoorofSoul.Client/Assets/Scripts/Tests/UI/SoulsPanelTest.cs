using UnityEngine;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Client.Global;

public class SoulsPanelTest : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        Global.Player.LoginResponse(1, "測試帳號", "測試暱稱", DoorofSoul.Protocol.Language.SupportLauguages.Chinese_Traditional, 1);
        Global.Player.ActiveAnswer(new Answer(1, 3, Global.Player));
        Global.Player.Answer.LoadSouls(new List<Soul>
        {
            new Soul(1, Global.Player.Answer, "測試1"),
            new Soul(2, Global.Player.Answer, "測試2"),
            new Soul(3, Global.Player.Answer, "測試3"),
            new Soul(4, Global.Player.Answer, "測試4"),
            new Soul(5, Global.Player.Answer, "測試5"),
            new Soul(6, Global.Player.Answer, "測試6")
        });
        Global.Player.Answer.LoadSouls(new List<Soul>
        {
            new Soul(7, Global.Player.Answer, "測試7"),
            new Soul(8, Global.Player.Answer, "測試8"),
        });
    }
}
