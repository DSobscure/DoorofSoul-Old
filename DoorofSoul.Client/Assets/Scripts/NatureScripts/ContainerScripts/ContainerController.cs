using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Library.General.NatureComponents;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.NatureScripts.ContainerScripts
{
    class ContainerController : MonoBehaviour, IContainerController
    {
        protected Container container;
        protected Canvas containerCanvus;
        protected Text nameText;

        public Container Container { get { return container; } }

        public GameObject GameObject { get { return gameObject; } }

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
    }
}
