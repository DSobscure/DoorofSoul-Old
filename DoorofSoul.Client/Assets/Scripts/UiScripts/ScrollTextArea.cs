using UnityEngine;

namespace DoorofSoul.Client.Scripts.UiScripts
{
    public class ScrollTextArea : MonoBehaviour
    {
        public float verticalDelta;
        public Vector2 Position
        {
            get
            {
                return new Vector2(transform.position.x - size.x / 2, Screen.height - transform.position.y - size.y / 2);
            }
        }
        public Vector2 size;
        public GUIStyle style;

        public Vector2 scrollbarPosition;
        public Vector2 scrollbarSize;
        public Vector2 textAreaPosition;
        public Vector2 textAreaSize;
        public GUIStyle textAreaStyle;

        public float handleSize;
        public float topValue;
        public float bottomValue;
        Vector2 scrollPosition;

        private string text = "";

        void OnGUI()
        {
            GUI.BeginGroup(new Rect(Position, size), style);
            verticalDelta = GUI.VerticalScrollbar(new Rect(scrollbarPosition, scrollbarSize), verticalDelta, handleSize, topValue, bottomValue);
            GUI.TextArea(new Rect(textAreaPosition + new Vector2(0, verticalDelta), textAreaSize), text, textAreaStyle);
            GUI.EndGroup();
        }

        public void ShowText(string text)
        {
            this.text = text;
            topValue = (textAreaSize.y > size.y) ? (textAreaSize.y - size.y + textAreaStyle.fontSize * 2) : 0;
            bottomValue = 0;
            verticalDelta = bottomValue;
        }
    }
}
