using Quiz.Data.Model.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Data.Service
{
    public class UserService : Repository<User>
    {
        public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
