namespace DoorofSoul.Database.DatabaseElements.Authentications
{
    public abstract class PlayerAuthentication
    {
        public abstract bool LoginCheck(string account, string password);
    }
}
