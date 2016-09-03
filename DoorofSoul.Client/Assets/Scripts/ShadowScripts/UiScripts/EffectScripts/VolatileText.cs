using UnityEngine;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.EffectScripts
{
    public class VolatileText : MonoBehaviour
    {
        private float lifeTime = 1;
        private Text text;

        void Update()
        {
            lifeTime -= Time.deltaTime;
            if(lifeTime <= 0)
            {
                Destroy(gameObject);
            }
            transform.localPosition += Vector3.up * Time.deltaTime;
        }

        public void Initial(float lifeTime, string displayText)
        {
            text = GetComponent<Text>();
            this.lifeTime = lifeTime;
            text.text = displayText;
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = Vector3.zero;
        }
    }
}
