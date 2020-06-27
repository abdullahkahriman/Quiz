using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Quiz.UI.Helper
{
    public static class Helper
    {
        private static IHttpContextAccessor _accessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }
        private static HttpContext Current => _accessor.HttpContext;

        public static string SetActiveClassName(string name)
        {
            return Current.Request.Path.ToString().ToLower().Equals(name.ToLower()) ? "active" : string.Empty;
        }

        public static string SetActiveClassName(string[] names)
        {
            return names.Contains(Current.Request.Path.ToString().ToLower()) ? "active" : string.Empty;
        }
    }
}