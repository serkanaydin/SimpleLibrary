using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence
{
    public class MainDbContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        private readonly IConfiguration configuration;

        public MainDbContext(DbContextOptions<MainDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseLoggerFactory(loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookType>()
                .HasMany(bt=>bt.Books)
                .WithOne(b=>b.BookType)
                .HasForeignKey(b=>b.BookTypeId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}