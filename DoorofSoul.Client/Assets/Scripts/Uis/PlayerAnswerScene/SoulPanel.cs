using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Library.General;

public class SoulPanel : MonoBehaviour
{
    private Text soulIDText;
    private Text soulNameText;

    public void Show(Soul soul)
    {
        soulIDText = transform.FindChild("SoulIDText").GetComponent<Text>();
        soulNameText = transform.FindChild("SoulNameText").GetComponent<Text>();
        soulIDText.text = soul.SoulID.ToString();
        soulNameText.text = soul.SoulName;
    }
}
