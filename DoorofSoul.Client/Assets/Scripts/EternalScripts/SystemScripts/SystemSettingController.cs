using UnityEngine;

namespace DoorofSoul.Client.Scripts.EternalScripts.SystemScripts
{
    public class SystemSettingController : MonoBehaviour
    {
        void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
