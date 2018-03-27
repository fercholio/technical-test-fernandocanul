using techical.test.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using technical.test.Model;

namespace techical.test.Services
{
    public class UserService: ServiceBase<User>, IUserService
    {
        public UserService(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        { }
    }
}
