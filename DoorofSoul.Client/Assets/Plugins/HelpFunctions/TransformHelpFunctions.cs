using UnityEngine;

namespace DoorofSoul.Client.HelpFunctions
{
    public static class TransformHelpFunctions
    {
        public static void ClearChild(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
