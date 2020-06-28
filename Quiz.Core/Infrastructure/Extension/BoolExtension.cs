namespace Quiz.Core
{
    public static class BoolExtension
    {
        public static bool IsPasswordLength(this string pass)
        {
            if(string.IsNullOrEmpty(pass) || pass.Length < 6)
            {
                return false;
            }

            return true;
        }
    }
}