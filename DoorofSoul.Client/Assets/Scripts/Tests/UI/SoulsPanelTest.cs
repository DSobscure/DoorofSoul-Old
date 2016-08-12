using UnityEngine;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Client.Global;

public class SoulsPanelTest : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        Global.Player.LoadPlayer(1, "測試帳號", "測試暱稱", DoorofSoul.Protocol.Language.SupportLauguages.Chinese_Traditional, 1);
        Global.Player.ActiveAnswer(new Answer(1, 3, Global.Player));
    }
}
