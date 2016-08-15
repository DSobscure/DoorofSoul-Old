using DoorofSoul.Database.DatabaseElements.Authentications;

namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class AuthenticationManager
    {
        public PlayerAuthentication PlayerAuthentication { get; protected set; }
    }
}
