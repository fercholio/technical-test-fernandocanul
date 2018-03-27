using techical.test.Data.Infrastructure;
using technical.test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techical.test.Services
{
    public class SessionService: ServiceBase<Session>, ISessionService
    {
        public SessionService(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        { }
    }
}
