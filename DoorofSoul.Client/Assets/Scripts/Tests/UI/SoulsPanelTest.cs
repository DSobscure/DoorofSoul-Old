using UnityEngine;
using System.Collections.Generic;
using DoorofSoul.Library.General;

public class SoulsPanelTest : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        Global.Player = new Player(1, "測試帳號", "測試暱稱", DoorofSoul.Protocol.Communication.SupportLauguages.Chinese_Traditional, null, 1);
        Global.Player.ActiveAnswer(new Answer(1, 3, Global.Player));
        Global.Player.Answer.LoadSouls(new List<Soul>
        {
            new Soul(1, 1, "測試1"),
            new Soul(2, 1, "測試2"),
            new Soul(3, 1, "測試3"),
            new Soul(4, 1, "測試4"),
            new Soul(5, 1, "測試5"),
            new Soul(6, 1, "測試6")
        });
        Global.Player.Answer.LoadSouls(new List<Soul>
        {
            new Soul(7, 1, "測試7"),
            new Soul(8, 1, "測試8"),
        });
    }
}
