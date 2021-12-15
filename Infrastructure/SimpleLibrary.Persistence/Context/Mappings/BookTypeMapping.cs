using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence.Mappings
{
    public sealed class BookTypeMapping : IEntityTypeConfiguration<BookType>
    {
        /// <summary>Configures the entity of type <span class="typeparameter">TEntity</span>.</summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<BookType> builder)
        {
            builder.ToTable("BookType");
            builder.Property(e => e.Id).HasColumnType("bigint");
            builder.Property(e => e.Type).HasColumnType("varchar(100)");
            
            builder.HasMany(t => t.Books)
                .WithOne(q=>q.BookType)
                .HasForeignKey(g => g.BookTypeId);

        }
    }
}