namespace DoorofSoul.Protocol.Communication
{
    public enum OperationCode : byte
    {
        FetchData,
        PlayerLogin,
        PlayerLogout,
        CreateSoul,
        DeleteSoul,
        ActivateSoul,
        ReportObserveEntityResult
    }
}
