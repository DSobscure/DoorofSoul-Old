using UnityEngine;

namespace DoorofSoul.Client.Scripts.SystemScripts
{
    public class SystemSettingController : MonoBehaviour
    {
        void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
