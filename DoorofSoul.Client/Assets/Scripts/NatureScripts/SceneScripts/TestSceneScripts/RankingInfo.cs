using UnityEngine;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.TestSceneScripts
{
    public class RankingInfo : MonoBehaviour
    {
        private Text nameText;
        private Text totalAbilitiesText;

        public void Initial(string name, int abilities)
        {
            nameText = transform.FindChild("NameText").GetComponent<Text>();
            totalAbilitiesText = transform.FindChild("TotalAbilitiesText").GetComponent<Text>();

            nameText.text = name;
            totalAbilitiesText.text = abilities.ToString();
        }
    }
}
