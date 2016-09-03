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

        void Update()
        {
            nameText.transform.LookAt(Camera.main.transform);
            nameText.transform.Rotate(0, 180, 0);
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
            container.Attributes.OnLifePointChange += OnLifePointChange;

            container.ShooterAbilities.OnDamageChange += OnBulletDamageChange;
            container.ShooterAbilities.OnMoveSpeedChange += OnMoveSpeedChange;
            container.ShooterAbilities.OnBulletSpeedChange += OnBulletSpeedChange;
            container.ShooterAbilities.OnTransparancyChange += OnTransparancyChange;

            OnLifePointChange(container.Attributes.LifePoint);
            OnBulletDamageChange(container.ShooterAbilities.Damage);
            OnMoveSpeedChange(container.ShooterAbilities.MoveSpeed);
            OnBulletSpeedChange(container.ShooterAbilities.BulletSpeed);
            OnTransparancyChange(container.ShooterAbilities.Transparancy);
        }
        private void OnLifePointChange(decimal value)
        {
            float lifePointRatio = Convert.ToSingle(value / container.Attributes.MaxLifePoint);
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.5f,0.5f,0.5f) * lifePointRatio);
        }
        private void OnBulletDamageChange(int damage)
        {
            Color originColor = gameObject.GetComponent<Renderer>().material.GetColor("_Color");
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(damage / 5f, originColor.g, originColor.b, originColor.a));
        }
        private void OnMoveSpeedChange(int speed)
        {
            Color originColor = gameObject.GetComponent<Renderer>().material.GetColor("_Color");
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(originColor.r, speed / 5f, originColor.b, originColor.a));
        }
        private void OnBulletSpeedChange(int speed)
        {
            Color originColor = gameObject.GetComponent<Renderer>().material.GetColor("_Color");
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(originColor.r, originColor.g, speed / 5f, originColor.a));
        }
        private void OnTransparancyChange(int transparancy)
        {
            Color originColor = gameObject.GetComponent<Renderer>().material.GetColor("_Color");
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(originColor.r, originColor.g, originColor.b, 1.001f - transparancy / 5f));
        }
    }
}
