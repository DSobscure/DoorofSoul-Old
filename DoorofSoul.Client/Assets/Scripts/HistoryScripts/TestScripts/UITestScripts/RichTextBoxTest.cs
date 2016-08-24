using UnityEngine;
using System.Text;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.UITestScripts
{
    public class RichTextBoxTest : MonoBehaviour
    {
        [SerializeField]
        private GUIStyle style;
        float scrollposition;
        void OnGUI()
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100; i++)
                sb.AppendLine("<color=yellow>RICH</color>");
            GUI.TextArea(new Rect(0, 0, 100, 500), sb.ToString(), style);
            scrollposition = GUI.VerticalScrollbar(new Rect(200, 200, 50, 500), scrollposition, 1.0F, 100.0F, 0.0F);
        }
    }
}
