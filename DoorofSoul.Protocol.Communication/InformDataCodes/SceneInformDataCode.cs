namespace DoorofSoul.Protocol.Communication.InformDataCodes
{
    public enum SceneInformDataCode : byte
    {
        EntityEnter,
        EntityExit,
        ContainerEnter,
        ContainerExit,
        BroadcastMessage,
        SynchronizeEntityPosition,
        SynchronizeEntityRotation,
        ItemEntityChange,
        ContainerLifePointChange,
    }
}
