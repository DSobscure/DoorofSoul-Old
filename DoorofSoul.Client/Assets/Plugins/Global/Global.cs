using DoorofSoul.Client.Communication;
using DoorofSoul.Client.Communication.Managers;
using DoorofSoul.Library.General;

public static class Global
{
    public static readonly OperationManagers OperationManagers;
    public static readonly ResponseManagers ResponseManagers;
    public static readonly EventManagers EventManagers;
    public static readonly SystemManagers SystemManagers;
    public static readonly VersionManager VersionManager;
    public static readonly PhotonService PhotonService;

    public static Player Player { get; set; }

    static Global()
    {
        OperationManagers = new OperationManagers();
        ResponseManagers = new ResponseManagers();
        EventManagers = new EventManagers();
        SystemManagers = new SystemManagers();
        VersionManager = new VersionManager();
        PhotonService = new PhotonService("DoorofSoul.Server", "doorofsoul.duckdns.org", 5055);

        EventManagers.EventManager.BindManagers();
        SystemManagers.UsingLauguage = DoorofSoul.Protocol.Communication.SupportLauguages.Chinese_Traditional;
    }
}
