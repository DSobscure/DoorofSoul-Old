﻿using DoorofSoul.Protocol;
using UnityEngine;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.SceneScripts.PlayerAnswerSceneScripts
{
    public class CreateSoulPanel : MonoBehaviour
    {
        private InputField soulNameInputField;
        private Dropdown mainSoulTypeDropdown;

        public string SoulName { get { return soulNameInputField.text; } }
        public SoulKernelType MainSoulType { get { return (SoulKernelType)mainSoulTypeDropdown.value; } }

        void Start()
        {
            soulNameInputField = transform.FindChild("SoulNameInputField").GetComponent<InputField>();
            mainSoulTypeDropdown = transform.FindChild("MainSoulTypeDropdown").GetComponent<Dropdown>();
        }
    }
}