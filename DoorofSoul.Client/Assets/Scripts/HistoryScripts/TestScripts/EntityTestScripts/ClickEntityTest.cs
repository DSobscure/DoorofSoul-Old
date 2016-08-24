using DoorofSoul.Client.Scripts.NatureScripts.EntityScripts;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.EntityTestScripts
{
    public class ClickEntityTest : MonoBehaviour
    {
        private Renderer hoveredRenderer;

        void Update()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.GetComponent<EntityController>() != null)
                {
                    SelectRenderer(hit.transform.GetComponent<Renderer>());
                }
            }
            else
            {
                SelectRenderer(null);
            }
        }

        private void SelectRenderer(Renderer renderer)
        {
            if(renderer != null)
            {
                if (hoveredRenderer != null)
                {
                    hoveredRenderer.material.SetColor("_EmissionColor", Color.black);
                }
                hoveredRenderer = renderer;
                hoveredRenderer.material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));
            }
            else
            {
                if (hoveredRenderer != null)
                {
                    hoveredRenderer.material.SetColor("_EmissionColor", Color.black);
                    hoveredRenderer = null;
                }
            }
        }
    }
}
