using UnityEngine;

public class PlayerAnswerController : MonoBehaviour
{
    [SerializeField]
    private CreateSoulPanel createSoulPanel;
    public void CreateSoul()
    {
        Global.OperationManagers.OperationManager.CreateSoul(createSoulPanel.SoulName);
    }
    public void DeleteSoul(int soulID)
    {
        Global.OperationManagers.OperationManager.DeleteSoul(soulID);
    }
    public void ActivateSoul(int soulID)
    {
        Global.OperationManagers.OperationManager.ActivateSoul(soulID);
    }
    public void Logout()
    {
        Global.OperationManagers.OperationManager.PlayerLogout();
    }
}
