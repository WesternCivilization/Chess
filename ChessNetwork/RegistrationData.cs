using System;

namespace Chess
{
    [Serializable]
    public class RegistrationData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }

        public RegistrationData( string login, string password, string fullName, int age )
        {
            Login = login;
            Password = password;
            FullName = fullName;
            Age = age;
        }

        public override string ToString()
        {
            return FullName + " (" + Login + ")";
        }
    }

    public enum RegistrationResult
    {
        OK, LoginAllreadyExist, UnknownError
    }
}
