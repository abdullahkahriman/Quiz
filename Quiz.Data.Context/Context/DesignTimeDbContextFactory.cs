using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Quiz.Data.Context.Context;
using System.IO;

namespace Quiz.Data.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<QuizDBContext>
    {
        public QuizDBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<QuizDBContext>();
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Quiz.API"))
                .AddJsonFile("appsettings.json")
                .Build();
            builder.UseSqlServer(configuration.GetConnectionString("DB"));
            return new QuizDBContext(builder.Options);
        }
    }
}