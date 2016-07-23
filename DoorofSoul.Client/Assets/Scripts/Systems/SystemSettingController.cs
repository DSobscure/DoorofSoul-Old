using UnityEngine;

public class SystemSettingController : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
