using DoorofSoul.Library.General;
using UnityEngine;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.UIScripts.PlayerPanelScripts
{
    public class SoulPanel : MonoBehaviour
    {
        private Soul soul;

        private Slider corePointSlider;
        private Slider spiritPointSlider;
        private Slider understandingPointSlider;

        public void Setup(Soul soul)
        {
            if (this.soul == null)
            {
                this.soul = soul;
                SetupSliders(true);
                this.soul.Attributes.OnCorePointChange += UpdateCorePoint;
                this.soul.Attributes.OnSpiritPointChange += UpdateSpiritPoint;
                //this.soul.Attributes.OnExperienceChange += UpdateExperiencePoint;
            }
            else
            {
                this.soul.Attributes.OnCorePointChange -= UpdateCorePoint;
                this.soul.Attributes.OnSpiritPointChange -= UpdateSpiritPoint;
                //this.soul.Attributes.OnExperienceChange -= UpdateExperiencePoint;
                this.soul = soul;
                SetupSliders(false);
                this.soul.Attributes.OnCorePointChange += UpdateCorePoint;
                this.soul.Attributes.OnSpiritPointChange += UpdateSpiritPoint;
                //this.soul.Attributes.OnExperienceChange += UpdateExperiencePoint;
            }
        }

        private void SetupSliders(bool shouldLoadSliders)
        {
            if (shouldLoadSliders)
            {
                corePointSlider = transform.FindChild("CorePointSlider").GetComponent<Slider>();
                spiritPointSlider = transform.FindChild("SpiritPointSlider").GetComponent<Slider>();
                understandingPointSlider = transform.FindChild("UnderstandingPointSlider").GetComponent<Slider>();
            }
            corePointSlider.maxValue = (float)soul.Attributes.MaxCorePoint;
            spiritPointSlider.maxValue = (float)soul.Attributes.MaxSpiritPoint;
            UpdateCorePoint(soul.Attributes.CorePoint);
            UpdateSpiritPoint(soul.Attributes.SpiritPoint);
        }

        private void UpdateCorePoint(decimal value)
        {
            corePointSlider.value = (float)value;
            corePointSlider.GetComponentInChildren<Text>().text = string.Format("{0:N2}/{1:N2}", value, soul.Attributes.MaxCorePoint);
        }
        private void UpdateSpiritPoint(decimal value)
        {
            spiritPointSlider.value = (float)value;
            spiritPointSlider.GetComponentInChildren<Text>().text = string.Format("{0:N2}/{1:N2}", value, soul.Attributes.MaxSpiritPoint);
        }
        private void UpdateUnderstandingPoint(int value)
        {
            understandingPointSlider.value = value;
            //understandingPointSlider.GetComponentInChildren<Text>().text = string.Format("{0:N2}/{1:N2}", value, soul.Attributes.MaxExperience);
        }
    }
}
