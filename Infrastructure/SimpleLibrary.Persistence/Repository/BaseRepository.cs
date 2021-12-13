using Microsoft.EntityFrameworkCore;

namespace SimpleLibrary.Persistence.Repository
{
    public abstract class BaseRepository
    {
        public BaseRepository(DbContext context)
        {
            Context = context;
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            Context.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected DbContext Context { get; }

        protected DbSet<T> Set<T>() where T : class
        {
            return Context.Set<T>();
        }

        protected int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}