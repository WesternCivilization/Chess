using System;

namespace Chess
{
    [Serializable]
    public class SignInData
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public SignInData( string login, string password )
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return "Login: " + Login + "\nPassword: " + Password;
        }
    }
    
    public enum SignInResult
    {
        OK, InvalidPasswordOrLogin, UnknownError
    }
}
