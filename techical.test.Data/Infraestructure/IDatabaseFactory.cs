using techical.test.Data.Contexts;
using System;

namespace techical.test.Data.Infrastructure
{
    public interface IDatabaseFactory: IDisposable
    {
        TechnicalTestContext Get();
    }
}
