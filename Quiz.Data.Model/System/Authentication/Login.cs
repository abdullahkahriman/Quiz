namespace Quiz.Data.Model.System.Authentication
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        public string Go { get; set; }
    }
}