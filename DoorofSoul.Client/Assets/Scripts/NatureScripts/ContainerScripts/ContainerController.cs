using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using System;
using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.EffectScripts;

namespace DoorofSoul.Client.Scripts.NatureScripts.ContainerScripts
{
    class ContainerController : MonoBehaviour, IContainerController
    {
        protected Container container;
        protected Canvas containerCanvus;
        protected Text nameText;
        [SerializeField]
        private VolatileText numberText;

        public Container Container { get { return container; } }

        public GameObject GameObject { get { return gameObject; } }

        void Update()
        {
            if(containerCanvus != null)
                containerCanvus.transform.LookAt(Camera.main.transform);
        }

        public void BindContainer(Container container)
        {
            this.container = container;
            containerCanvus = transform.FindChild("ContainerCanvas").GetComponent<Canvas>();
            containerCanvus.worldCamera = Camera.main;
            nameText = containerCanvus.transform.FindChild("NameText").GetComponent<Text>();
            if(container.FirstSoul != null)
            {
                nameText.text = container.FirstSoul.SoulName;
            }
            else
            {
                nameText.text = container.ContainerName;
            }
        }

        public void ShowLifePointDelta(decimal delta)
        {
            Debug.LogFormat("Delta {0}", delta);
            VolatileText text = Instantiate(numberText);
            text.transform.SetParent(containerCanvus.transform);
            text.Initial(0.5f, string.Format("{0:N2}", delta));
            text.GetComponent<RectTransform>().localRotation = nameText.GetComponent<RectTransform>().localRotation;
        }
    }
}
