using UnityEngine;

namespace DoorofSoul.Client.HelpFunctions
{
    public static class TransformHelpFunctions
    {
        public static void ClearChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
