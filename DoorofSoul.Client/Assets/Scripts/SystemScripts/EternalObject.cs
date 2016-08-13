using UnityEngine;

namespace DoorofSoul.Client.Scripts.SystemScripts
{
    public class EternalObject : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
