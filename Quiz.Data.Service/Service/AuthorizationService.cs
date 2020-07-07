using Microsoft.EntityFrameworkCore;
using Quiz.Data.Context.Context;
using Quiz.Data.Model.System;
using Quiz.Data.Model.System.Authentication;
using Quiz.Data.Model.System.Authorization;
using Quiz.Data.Service.Interface;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Core;

namespace Quiz.Data.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly QuizDBContext _context;
        public AuthorizationService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<QuizDBContext>();
        }

        public Authorize Get(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                Token modelToken = token.FromRijndael<Token>();
                if (modelToken.ExpireDate > DateTime.Now)
                {
                    User user = _context.User.FirstOrDefault(c => c.ID.Equals(modelToken.UserID));
                    long[] roles = _context.UserRole.Where(c => c.UserID == user.ID)
                        .Select(c => c.RoleID)
                        .ToArray();

                    if (user != null && roles != null && roles.Length > 0)
                    {
                        return new Authorize()
                        {
                            User = user,
                            AuthorizedActions = _context.RoleSystemAction
                            .Include(c => c.SystemAction)
                            .Where(c => roles.Contains(c.RoleID))
                            .Select(c => c.SystemAction).ToList()
                        };
                    }
                }
            }

            return null;
        }
    }
}
