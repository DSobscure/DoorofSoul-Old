using DoorofSoul.Client.Protocol.Language;
using DoorofSoul.Client.Scripts.UiScripts.ExtraPanelScripts;
using DoorofSoul.Library.General;
using UnityEngine;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.UiScripts.PlayerPanelScripts
{
    public class ContainerPanel : MonoBehaviour
    {
        private Container container;

        private Text containerNameText;
        private Text containerLevelText;

        private Slider lifePointSlider;
        private Slider energyPointSlider;
        private Slider experienceSlider;

        private Button inventoryButton;

        [SerializeField]
        private InventoryPanel inventoryPanelPrefab;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ShowInventoryPanel();
            }
        }

        public void Setup(Container container)
        {
            if (this.container == null)
            {
                this.container = container;
                SetupSliders(true);
                SetupInventory();
                SetupNameAndLevel();
                this.container.Attributes.OnLifePointChange += UpdateLifePoint;
                this.container.Attributes.OnEnergyPointChange += UpdateEnergyPoint;
                this.container.Attributes.OnExperienceChange += UpdateExperiencePoint;
            }
            else
            {
                this.container.Attributes.OnLifePointChange -= UpdateLifePoint;
                this.container.Attributes.OnEnergyPointChange -= UpdateEnergyPoint;
                this.container.Attributes.OnExperienceChange -= UpdateExperiencePoint;
                this.container = container;
                SetupSliders(false);
                SetupInventory();
                SetupNameAndLevel();
                this.container.Attributes.OnLifePointChange += UpdateLifePoint;
                this.container.Attributes.OnEnergyPointChange += UpdateEnergyPoint;
                this.container.Attributes.OnExperienceChange += UpdateExperiencePoint;
            }
        }

        private void SetupSliders(bool shouldLoadSliders)
        {
            if (shouldLoadSliders)
            {
                lifePointSlider = transform.FindChild("LifePointSlider").GetComponent<Slider>();
                energyPointSlider = transform.FindChild("EnergyPointSlider").GetComponent<Slider>();
                experienceSlider = transform.FindChild("ExperienceSlider").GetComponent<Slider>();
            }
            lifePointSlider.maxValue = (float)container.Attributes.MaxLifePoint;
            energyPointSlider.maxValue = (float)container.Attributes.MaxEnergyPoint;
            experienceSlider.maxValue = (float)container.Attributes.MaxExperience;
            UpdateLifePoint(container.Attributes.LifePoint);
            UpdateEnergyPoint(container.Attributes.EnergyPoint);
            UpdateExperiencePoint(container.Attributes.Experience);
        }
        private void SetupInventory()
        {
            inventoryButton = transform.FindChild("InventoryButton").GetComponent<Button>();
            inventoryButton.onClick.AddListener(() => ShowInventoryPanel());
            inventoryButton.GetComponentInChildren<Text>().text = UILanguageSeletor.Instance[container.UsingLanguage]["Inventory"];
        }
        private void SetupNameAndLevel()
        {
            containerNameText = transform.FindChild("ContainerNameText").GetComponent<Text>();
            containerLevelText = transform.FindChild("ContainerLevelText").GetComponent<Text>();

            containerNameText.text = container.ContainerName;
            containerLevelText.text = string.Format("LV.{0}", container.Attributes.Level);
        }

        private void UpdateLifePoint(decimal value)
        {
            lifePointSlider.value = (float)value;
            lifePointSlider.GetComponentInChildren<Text>().text = string.Format("{0:N2}/{1:N2}", value, container.Attributes.MaxLifePoint);
        }
        private void UpdateEnergyPoint(decimal value)
        {
            energyPointSlider.value = (float)value;
            energyPointSlider.GetComponentInChildren<Text>().text = string.Format("{0:N2}/{1:N2}", value, container.Attributes.MaxEnergyPoint);
        }
        private void UpdateExperiencePoint(int value)
        {
            experienceSlider.value = value;
            experienceSlider.GetComponentInChildren<Text>().text = string.Format("{0:N2}/{1:N2}", value, container.Attributes.MaxExperience);
        }
        private void ShowInventoryPanel()
        {
            if (transform.parent.parent.FindChild("InventoryPanel") == null)
            {
                InventoryPanel inventoryPanel = Instantiate(inventoryPanelPrefab);
                inventoryPanel.name = "InventoryPanel";
                inventoryPanel.transform.SetParent(transform.parent.parent);
                RectTransform rectTransform = inventoryPanel.GetComponent<RectTransform>();
                rectTransform.localScale = Vector3.one;
                rectTransform.localPosition = Vector2.zero;
                inventoryPanel.BindInventory(container.Inventory);
            }
            else
            {
                transform.parent.parent.FindChild("InventoryPanel").GetComponent<InventoryPanel>().Close();
            }
        }
    }
}
