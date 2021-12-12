using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence.Mappings
{
    public class AuthorMapping: IEntityTypeConfiguration<Author>
    {
        /// <summary>Configures the entity of type <span class="typeparameter">TEntity</span>.</summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");
            builder.Property(e => e.Id).HasColumnType("int(11)");
            builder.Property(e => e.Name).HasColumnType("varchar(100)");
        }
    }
}