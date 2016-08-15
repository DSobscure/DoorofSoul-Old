using DoorofSoul.Library.General;
using UnityEngine;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.UIScripts.PlayerPanelScripts
{
    public class SoulPanel : MonoBehaviour
    {
        private Soul soul;

        private Text soulNameText;
        private Text phaseLevelText;

        private Slider corePointSlider;
        private Slider spiritPointSlider;
        private Slider understandingPointSlider;
        

        public void Setup(Soul soul)
        {
            if (this.soul == null)
            {
                this.soul = soul;
                SetupSliders(true);
                SetupNameAndLevel();
                this.soul.Attributes.OnCorePointChange += UpdateCorePoint;
                this.soul.Attributes.OnSpiritPointChange += UpdateSpiritPoint;
                this.soul.Attributes.Phases[soul.Attributes.CurrentPhaseLevel].OnUnderstandingPointChange += UpdateUnderstandingPoint;
            }
            else
            {
                this.soul.Attributes.OnCorePointChange -= UpdateCorePoint;
                this.soul.Attributes.OnSpiritPointChange -= UpdateSpiritPoint;
                this.soul.Attributes.Phases[soul.Attributes.CurrentPhaseLevel].OnUnderstandingPointChange -= UpdateUnderstandingPoint;
                this.soul = soul;
                SetupSliders(false);
                SetupNameAndLevel();
                this.soul.Attributes.OnCorePointChange += UpdateCorePoint;
                this.soul.Attributes.OnSpiritPointChange += UpdateSpiritPoint;
                this.soul.Attributes.Phases[soul.Attributes.CurrentPhaseLevel].OnUnderstandingPointChange += UpdateUnderstandingPoint;
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
            understandingPointSlider.maxValue = soul.Attributes.Phases[soul.Attributes.CurrentPhaseLevel].MaxUnderstandingPoint;

            UpdateCorePoint(soul.Attributes.CorePoint);
            UpdateSpiritPoint(soul.Attributes.SpiritPoint);
            UpdateUnderstandingPoint(soul.Attributes.Phases[soul.Attributes.CurrentPhaseLevel].UnderstandingPoint);
        }
        private void SetupNameAndLevel()
        {
            soulNameText = transform.FindChild("SoulNameText").GetComponent<Text>();
            phaseLevelText = transform.FindChild("PhaseLevelText").GetComponent<Text>();

            soulNameText.text = soul.SoulName;
            phaseLevelText.text = string.Format("LV.{0}", soul.Attributes.CurrentPhaseLevel);
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
            understandingPointSlider.GetComponentInChildren<Text>().text = string.Format("{0:N2}/{1:N2}", value, soul.Attributes.Phases[soul.Attributes.CurrentPhaseLevel].MaxUnderstandingPoint);
        }
    }
}
