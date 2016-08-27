using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DoorofSoul.Client.Interfaces
{
    public interface IInputScenario
    {
        event Action<KeyCode> OnKeyDown;
        event Action<KeyCode> OnKeyUp;
    }
}
