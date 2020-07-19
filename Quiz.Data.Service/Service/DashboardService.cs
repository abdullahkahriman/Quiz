using Quiz.Data.Context.Context;
using System;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Core;
using System.Linq;
using Quiz.Data.Service.Interface;

namespace Quiz.Data.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly QuizDBContext _context;
        public DashboardService(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<QuizDBContext>();
        }

        /// <summary>
        /// Get total count
        /// </summary>
        /// <returns></returns>
        public Result<object> GetCount()
        {
            Result<object> result;

            try
            {
                int totalQuestion = this._context.Question.Count(c => !c.IsDeleted);
                int totalUser = this._context.User.Count(c => !c.IsDeleted);

                object total = new
                {
                    totalQuestion,
                    totalUser
                };

                result = new Result<object>(true, "", total);
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }
    }
}
