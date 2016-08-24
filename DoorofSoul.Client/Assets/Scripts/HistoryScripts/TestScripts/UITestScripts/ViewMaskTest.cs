using UnityEngine;
using System.Collections;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.UITestScripts
{
    public class ViewMaskTest : MonoBehaviour
    {
        [SerializeField]
        private RectTransform mask;
        private Vector2 targetVector;
        private float deltaTime;
        public void Rotate(int direction)
        {
            deltaTime = 0;
            switch (direction)
            {
                case -1:
                    targetVector = new Vector2(-50, 0);
                    break;
                case 0:
                    targetVector = new Vector2(0, 0);
                    break;
                case 1:
                    targetVector = new Vector2(50, 0);
                    break;

            }
        }
        void Update()
        {
            mask.localPosition = Vector2.Lerp(mask.localPosition, targetVector, deltaTime);
            deltaTime += Time.deltaTime;
        }
        IEnumerator Left()
        {
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                mask.localPosition = Vector2.Lerp(mask.localPosition, new Vector2(-50, 0), i);
                yield return 0;
            }
        }
        IEnumerator Stop()
        {
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                mask.localPosition = Vector2.Lerp(mask.localPosition, new Vector2(0, 0), i);
                yield return 0;
            }
        }
        IEnumerator Right()
        {
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                mask.localPosition = Vector2.Lerp(mask.localPosition, new Vector2(50, 0), i);
                yield return 0;
            }
        }
    }
}
