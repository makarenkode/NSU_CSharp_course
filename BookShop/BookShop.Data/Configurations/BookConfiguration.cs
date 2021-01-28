using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data.Configurations
{
	[UsedImplicitly]
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable(nameof(Book), BSContext.DefaultSchemaName);
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id);

			builder.Property(x => x.Title).IsRequired();
			builder.Property(x => x.Genre).IsRequired();
			builder.Property(x => x.Price).IsRequired();
			builder.HasCheckConstraint("CK_BookShop.Book_Price", "[Price] > 0");

			builder.Property(x => x.IsNew).IsRequired();
			builder.Property(x => x.DateOfDelivery).IsRequired();
		}
	}
}
