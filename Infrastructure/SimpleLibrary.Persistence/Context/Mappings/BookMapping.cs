using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence.Mappings
{
    public sealed class BookMapping : IEntityTypeConfiguration<Book>
    {
        /// <summary>Configures the entity of type <span class="typeparameter">TEntity</span>.</summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.Property(e => e.Id).HasColumnType("bigint");
            builder.Property(e => e.AuthorId).HasColumnType("bigint");
            builder.Property(e => e.BookTypeId).HasColumnType("bigint");

            builder.Property(e => e.Name).HasColumnType("varchar(100)");
            builder.Property(e => e.Price).HasColumnType("decimal");
            builder.HasOne(q => q.BookType)
                .WithMany(p => p.Books)
                .HasForeignKey(d => d.BookTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}