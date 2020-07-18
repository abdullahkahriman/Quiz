using Microsoft.Extensions.DependencyInjection;
using Quiz.Data.Model.Entity;
using Quiz.Data.Model.System;
using Quiz.Data.Service.Interface;

namespace Quiz.Data.Service
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepositoryService(this IServiceCollection services)
        {
            services.AddDbContextService();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IRepository<User>, UserService>();
            services.AddTransient<IRepository<Question>, QuizService>();
            services.AddTransient<IRepository<Role>, RoleService>();
            services.AddTransient<IRepository<SystemAction>, SystemActionService>();
            return services;
        }
    }
}