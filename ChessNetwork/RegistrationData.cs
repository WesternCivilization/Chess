using System;

namespace Chess
{
    [Serializable]
    public class RegistrationData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public RegistrationData( string login, string password, string fullName )
        {
            Login = login;
            Password = password;
            FullName = fullName;
        }

        public override string ToString()
        {
            return Login + " (" + FullName + ")";
        }
    }

    public enum RegistrationResult
    {
        Unknown, OK, LoginAllreadyExist
    }
}
