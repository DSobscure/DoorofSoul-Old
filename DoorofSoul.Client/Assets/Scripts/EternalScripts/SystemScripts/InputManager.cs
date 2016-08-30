using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DoorofSoul.Client.Interfaces;
using System.Collections.Generic;

namespace DoorofSoul.Client.Scripts.EternalScripts.SystemScripts
{
    public class InputManager : EventTrigger, IInputScenario
    {
        private event Action<KeyCode> onKeyDown;
        public event Action<KeyCode> OnKeyDown { add { onKeyDown += value; } remove { onKeyDown -= value; } }

        private event Action<KeyCode> onKeyUp;
        public event Action<KeyCode> OnKeyUp { add { onKeyUp += value; } remove { onKeyUp -= value; } }

        private List<KeyCode> keyCodes;

        void Awake()
        {
            Global.Global.InputManager = this;
            keyCodes = new List<KeyCode>
            {
                KeyCode.Tab,
                KeyCode.Space,
                KeyCode.A,
                KeyCode.S,
                KeyCode.D,
                KeyCode.W,
                KeyCode.I,
                KeyCode.K
            };
        }

        void Update()
        {
            foreach (KeyCode key in keyCodes)
            {
                if (Input.GetKeyDown(key) && onKeyDown != null)
                    onKeyDown.Invoke(key);
                if (Input.GetKeyUp(key) && onKeyUp != null)
                {
                    onKeyUp.Invoke(key);
                }
            }
        }
    }
}
