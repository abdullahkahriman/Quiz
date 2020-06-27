using Microsoft.Extensions.DependencyInjection;
using Quiz.Data.Model.System;
using Quiz.Data.Service.Interface;

namespace Quiz.Data.Service
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepositoryService(this IServiceCollection services)
        {
            services.AddDbContextService();
            services.AddTransient<IRepository<User>, UserService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            return services;
        }
    }
}