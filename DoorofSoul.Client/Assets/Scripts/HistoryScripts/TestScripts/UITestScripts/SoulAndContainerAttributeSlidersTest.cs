using DoorofSoul.Library.General.MindComponents;
using DoorofSoul.Library.General.NatureComponents;
using System.Collections;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.UITestScripts
{
    public class SoulAndContainerAttributeSlidersTest : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(AttributeChange());
        }

        IEnumerator AttributeChange()
        {
            for (int i = 0; i < 1000; i++)
            {
                yield return new WaitForSeconds(1f);
                Container container = Global.Global.Seat.MainContainer;
                Soul soul = Global.Global.Seat.MainSoul;

                container.Attributes.LifePoint -= 5;
                container.Attributes.EnergyPoint -= 3;
                container.Attributes.Experience += 1;

                soul.Attributes.CorePoint -= 1;
                soul.Attributes.SpiritPoint -= 2;
            }
        }
    }
}
