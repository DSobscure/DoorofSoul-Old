using UnityEngine;
using UnityEngine.UI;

public class CreateSoulPanel : MonoBehaviour
{
    private InputField soulNameInputField;
    public string SoulName { get { return soulNameInputField.text; } }

    void Start()
    {
        soulNameInputField = transform.FindChild("SoulNameInputField").GetComponent<InputField>();
    }
}
