namespace Quiz.Data.Model.System.Authentication
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}