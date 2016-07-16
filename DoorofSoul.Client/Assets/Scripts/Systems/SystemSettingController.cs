using UnityEngine;
using System.Collections;

public class SystemSettingController : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
