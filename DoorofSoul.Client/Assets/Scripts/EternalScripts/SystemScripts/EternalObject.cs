using UnityEngine;

namespace DoorofSoul.Client.Scripts.EternalScripts.SystemScripts
{
    public class EternalObject : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
