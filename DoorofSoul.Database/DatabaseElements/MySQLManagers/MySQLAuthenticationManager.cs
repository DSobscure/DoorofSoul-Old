using DoorofSoul.Database.DatabaseElements.Authentications.MySQL;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers
{
    class MySQLAuthenticationManager : AuthenticationManager
    {
        public MySQLAuthenticationManager()
        {
            PlayerAuthentication = new MySQLPlayerAuthentication();
        }
    }
}
