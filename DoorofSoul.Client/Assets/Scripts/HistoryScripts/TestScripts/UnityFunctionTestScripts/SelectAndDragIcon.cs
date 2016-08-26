using UnityEngine;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.UnityFunctionTestScripts
{
    class SelectAndDragIcon : MonoBehaviour
    {
        private Button iconButton;
        private Button selectIcon;

        void Start()
        {
            iconButton = GetComponent<Button>();
            iconButton.onClick.AddListener(SelectIcon);
        }
        void OnGUI()
        {
            if(selectIcon != null)
            {
                RectTransform rectTransform = selectIcon.GetComponent<RectTransform>();
                Vector2 mousePosition = Event.current.mousePosition;
                float x = mousePosition.x - Screen.width / 2;
                float y = -(mousePosition.y - Screen.height / 2);
                rectTransform.localPosition = new Vector2(x, y);
            }
            if(Event.current.isMouse && Event.current.button == 0 && Event.current.clickCount > 1)
            {
                Debug.Log(Event.current.clickCount);
            }
        }
        private void SelectIcon()
        {
            selectIcon = Instantiate(iconButton);
            selectIcon.transform.SetParent(transform.parent);
            Destroy(selectIcon.GetComponent<SelectAndDragIcon>());
            selectIcon.onClick.AddListener(() => CancleSelect());
        }
        private void CancleSelect()
        {
            Destroy(selectIcon.gameObject);
            Debug.Log("DE");
        }
    }
}
