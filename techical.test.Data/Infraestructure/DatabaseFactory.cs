using techical.test.Data.Contexts;

namespace techical.test.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private TechnicalTestContext dataContext;

        public TechnicalTestContext Get()
        {
            return dataContext ?? (dataContext = new TechnicalTestContext());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
