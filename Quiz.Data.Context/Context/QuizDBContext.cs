using Microsoft.EntityFrameworkCore;
using Quiz.Data.Model.Entity;
using Quiz.Data.Model.System;

namespace Quiz.Data.Context.Context
{
    public class QuizDBContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleSystemAction> RoleSystemAction { get; set; }
        public DbSet<SystemAction> SystemAction { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswer { get; set; }
        public DbSet<UserAnswer> UserAnswer { get; set; }

        public QuizDBContext(DbContextOptions<QuizDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}