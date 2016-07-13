using DoorofSoul.Client.Communication;
using DoorofSoul.Client.Communication.Managers;

public static class Global
{
    public static readonly OperationManagers OperationManagers = new OperationManagers();
    public static readonly ResponseManagers ResponseManagers = new ResponseManagers();
    public static readonly EventManagers EventManagers = new EventManagers();
    public static readonly SystemManagers SystemManagers = new SystemManagers();
    public static readonly VersionManager VersionManager = new VersionManager();
    public static readonly PhotonService PhotonService = new PhotonService("DoorofSoul.Server", "doorofsoul.duckdns.org", 5055);
}
