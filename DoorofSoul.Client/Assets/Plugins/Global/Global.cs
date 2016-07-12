﻿using DoorofSoul.Client.Managers;

public static class Global
{
    public static readonly PhotonService Service = new PhotonService("DoorofSoul.Server", "doorofsoul.duckdns.org", 5055);
    public static readonly OperationManagers OperationManagers = new OperationManagers();
    public static readonly ResponseManagers ResponseManagers = new ResponseManagers();
    public static readonly EventManagers EventManagers = new EventManagers();
    public static readonly SystemManagers SystemManagers = new SystemManagers();
}
